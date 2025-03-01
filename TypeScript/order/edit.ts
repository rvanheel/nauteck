import Functions from "../account/Functions";

class OrderEdit {
  static NumberFormat = new Intl.NumberFormat('en-US', { style: 'decimal', minimumFractionDigits: 2, maximumFractionDigits: 2 });
  static async AddressCheck(): Promise<void> {
    const form = document.getElementById('form-order') as HTMLFormElement;
    const elements = form.elements;
    const postcodeElement = elements['zipcode'];
    const zipCode: string = postcodeElement.value.trim();
    const number: number = +elements['number'].value;

    elements['address'].value = '';
    elements['city'].value = '';
    elements['region'].value = '';
    if (!elements['country'].value.match(/Nederland/i)) return;

    const api = 'https://api.pdok.nl';
    const fields = 'woonplaatsnaam,straatnaam,huisnummer,postcode,provincienaam';

    const url = new URL('/bzk/locatieserver/search/v3_1/free', api);
    url.searchParams.append('q', `postcode:${zipCode}`);
    url.searchParams.append('fq', `huisnummer:${number}`);
    url.searchParams.append('fl', fields);
    url.searchParams.append('rows', '1');

    const response = await fetch(url.toString(), {
      method: 'GET',
      cache: 'no-cache',
      redirect: 'follow',
    });
    const json = await response.json();
    if (response.ok && json?.response?.numFound) {
      const record = json.response.docs[0];
      elements['address'].value = record.straatnaam;
      elements['city'].value = record.woonplaatsnaam;
      elements['region'].value = record.provincienaam;
      postcodeElement.value = record.postcode;
      return;
    }
  }  
  static CalculateTotal() {   
    
    let total = 0.0;
    const elements = document.querySelectorAll('[data-total]');
    elements.forEach((element) => {
      total += parseFloat(element.textContent.replace(',','')) || 0;
    });
    const discount = +Functions.GetHtmlInputElementById('discount').value;
    const totalAmount =  OrderEdit.NumberFormat.format(total - discount);
    Functions.SetHtmlTextContent('span[data-type="subtotal-price"]', OrderEdit.NumberFormat.format(total));
    Functions.SetHtmlTextContent('span[data-type="discount-price"]', `- ${OrderEdit.NumberFormat.format(discount)}`);
    Functions.SetHtmlTextContent('span[data-type="floor-order-total"]', totalAmount);

    Functions.GetHtmlInputElementById('Total').value = totalAmount;
  }
  static CallOutCostChanged(this: HTMLInputElement) {
    const price = +Functions.GetHtmlInputElement('[name="parts.CallOutCostPrice"]').value;
    const quantity = +this.value;
    const total = OrderEdit.NumberFormat.format(price * quantity);
    Functions.GetHtmlInputElement('[name="parts.CallOutCostTotal"]').value = total;
    Functions.SetHtmlTextContent('span[data-type="call-out-cost-price"]', total);

    OrderEdit.CalculateTotal();
  }
  static ColorChanged(this: HTMLSelectElement) {
    const above = Functions.GetSelectOption('Parts.floorColorAbove').dataset.exclusive;
    const beneath = Functions.GetSelectOption('Parts.floorColorBeneath').dataset.exclusive;

    if (above === "true" || beneath === "true"){
      const price = +Functions.GetHtmlInputElement('[name="parts.ColorPrice"]').value;
      const quantity = +Functions.GetHtmlInputElement('[name="parts.FloorQuantity"]').value;
      const total = Functions.GetHtmlInputElement('[name="parts.ColorTotal"]');
      total.value = OrderEdit.NumberFormat.format(price * quantity);
      Functions.SetHtmlTextContent('span[data-type="color-price"]', total.value);
    }
    OrderEdit.CalculateTotal();
  }
  static ConstructionChanged(this: HTMLSelectElement) {
    const selectedOption = this.options[this.selectedIndex];
    const price = +selectedOption.dataset.price;

    OrderEdit.SetPrice('parts.Construction', price);

    OrderEdit.CalculateTotal();
  }
  static async Delete(this: HTMLElement) {
    const id = window.location.pathname.split('/').pop();
    bootbox.confirm({
      message: `<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze order wilt verwijderen?</p><p><strong>LET OP!</strong> dit kan niet ongedaan worden gemaakt en alle gekoppelde items en documenten worden tevens verwijderd.</p></div>`,
      buttons: {
        confirm: {
          label: 'Ja',
          className: 'btn-danger'
        },
        cancel: {
          label: 'Nee',
          className: 'btn-secondary'
        }
      },
      callback: async (result) => {
        if (!result) return;
        Functions.ShowToastr();
        const button = this as HTMLButtonElement;
        const url = button.dataset.url;
        const response = await fetch(url, {
          method: 'DELETE',
          cache: 'no-cache',
          redirect: 'follow',
        });
        if (!response.ok) {
          Functions.RemoveToastr();
          Functions.ToastrError('Error', 'Er is een fout opgetreden bij het verwijderen van de order.');
          return;
        }
        window.location.replace('/');
      }
    });
  }
  static DesignChanged(this: HTMLSelectElement) {
    const selectedOption = this.options[this.selectedIndex];
    const price = +selectedOption.dataset.price;

    OrderEdit.SetPrice('parts.Design', price);

    OrderEdit.CalculateTotal();
  }
  static FreePriceChanged(this: HTMLInputElement) {
    document.querySelector('span[data-type="other-price"]').textContent = OrderEdit.NumberFormat.format(+this.value);
    OrderEdit.CalculateTotal();
  }
  static FloorChanged(this: HTMLSelectElement) {
    const selectedOption = this.options[this.selectedIndex];

    const price = +selectedOption.dataset.price;
    const quantity = Functions.GetHtmlInputElementById('Parts.floorQuantity') as HTMLInputElement;

    var floorTotal = OrderEdit.NumberFormat.format(+quantity.value * +price);
    Functions.GetHtmlInputElement('[name="parts.FloorPrice"]').value = OrderEdit.NumberFormat.format(price);
    Functions.GetHtmlInputElement('[name="parts.FloorTotal"]').value = floorTotal;
    Functions.SetHtmlTextContent('span[data-type="floor"]', selectedOption.textContent);
    Functions.SetHtmlTextContent('span[data-type="floor-quantity"]', OrderEdit.NumberFormat.format(+quantity.value));
    Functions.SetHtmlTextContent('span[data-type="floor-price"]', OrderEdit.NumberFormat.format(+price));
    Functions.SetHtmlTextContent('span[data-type="floor-sum"]', floorTotal);

    OrderEdit.CalculateTotal();
  }
  static Initialize() {
    document.getElementById("country").addEventListener('change', OrderEdit.AddressCheck, false);
    document.getElementById("zipcode").addEventListener('blur', OrderEdit.AddressCheck, false);
    document.getElementById("number").addEventListener('blur', OrderEdit.AddressCheck, false);
    document.getElementById("button-delete").addEventListener('click', OrderEdit.Delete, false);
    document.getElementById("Parts.Floor").addEventListener('change', OrderEdit.FloorChanged, false);
    document.querySelectorAll('input[data-type="logo"]').forEach((element) => element.addEventListener('change', OrderEdit.LogoChanged, false));

    document.querySelectorAll('select[name="parts.FloorColorAbove"],select[name="parts.Parts.floorColorBeneath"]').forEach((element) => element.addEventListener('change', OrderEdit.ColorChanged, false));
    
    Functions.GetHtmlInputElementById('discount').addEventListener('change', OrderEdit.CalculateTotal, false);
    Functions.GetHtmlInputElementById('freePrice').addEventListener('change', OrderEdit.FreePriceChanged, false);
    Functions.GetHtmlInputElementById('Parts.callOutCostQuantity').addEventListener('change', OrderEdit.CallOutCostChanged, false);	
    Functions.GetHtmlInputElementById('Parts.floorQuantity').addEventListener('change', OrderEdit.QuantityChanged, false);
    Functions.GetHtmlSelectElementById('Parts.design').addEventListener('change', OrderEdit.DesignChanged, false);
    Functions.GetHtmlSelectElementById('Parts.construction').addEventListener('change', OrderEdit.ConstructionChanged, false);
    Functions.GetHtmlSelectElementById('Parts.measurement').addEventListener('change', OrderEdit.MeasurementChanged, false);

    OrderEdit.InitForm();
  }
  static InitForm() {
    $("#form-order").validate({
      messages: {

      },
      rules: {
      },
      submitHandler(form, event) {
        event.preventDefault();
        const elements = form;
        var data = new FormData(form);
        const uri = new URL(form.action, window.location.origin);
        fetch(uri.href, {
          body: data,
          method: 'POST',
        }).then(response => {
          if (!response.ok) {
            Functions.ToastrError('Error', 'Er is een fout opgetreden bij het opslaan van de order.');
            return;
          }
          window.location.replace('/');
        });

      },
    });
  }
  static LogoChanged(this: HTMLInputElement) {
    const sum = document.querySelector(`span[data-sum="${this.id}`) as HTMLSpanElement;
    sum.textContent = OrderEdit.NumberFormat.format(+this.value * +this.dataset.price);

    let totalSum = 0;
    const sumElements = document.querySelectorAll('span[data-sum]');
    sumElements.forEach((element) => {
      totalSum += parseFloat(element.textContent) || 0;
    });
    Functions.GetHtmlInputElement('[name="parts.LogoTotal"]').value = OrderEdit.NumberFormat.format(totalSum);
    Functions.SetHtmlTextContent('[data-type="logo-price"]', OrderEdit.NumberFormat.format(totalSum));

    OrderEdit.CalculateTotal();
  }
  static MeasurementChanged(this: HTMLSelectElement) {
    const selectedOption = this.options[this.selectedIndex];
    const price = +selectedOption.dataset.price;

    OrderEdit.SetPrice('parts.Measurement', price);

    OrderEdit.CalculateTotal();
  }
  static SetPrice(identifier:string, price: number) {
    const priceFormat = OrderEdit.NumberFormat.format(price);
    Functions.GetHtmlInputElement(`[name="${identifier}Price"]`).value = priceFormat;
    Functions.GetHtmlInputElement(`[name="${identifier}Total"]`).value = priceFormat;
  }
  static QuantityChanged(this: HTMLInputElement) {
    Functions.GetHtmlSelectElementById('Parts.Floor').dispatchEvent(new Event('change'));
  }
}
document.addEventListener('DOMContentLoaded', OrderEdit.Initialize);
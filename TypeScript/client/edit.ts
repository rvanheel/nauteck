import Functions from "../account/Functions";

class ClientEdit {
  static abortController:AbortController = null;

  static async AddressCheck(): Promise<void> {
    if (ClientEdit.abortController) {
      ClientEdit.abortController.abort();
      console.error('aborting previous request');
    }
    ClientEdit.abortController = new AbortController();
    const signal = ClientEdit.abortController.signal;
    const form = document.getElementById('form-client') as HTMLFormElement;
    const elements = form.elements;
    const postcodeElement = elements['Zipcode'];
    const zipCode: string = postcodeElement.value.trim();
    const number: number = +elements['Number'].value;

    const straat = elements['Address'];
    const plaats = elements['City'];
    const provincie = elements['Region'];

    straat.value = '';
    plaats.value = '';
    provincie.value = '';

    const address = Functions.GetHtmlInputElementById('Address');
    const city = Functions.GetHtmlInputElementById('City');
    const region = Functions.GetHtmlInputElementById('Region');
    address.setAttribute('readonly','readonly');
    city.setAttribute('readonly','readonly');
    region.setAttribute('readonly','readonly');


    if (!elements['Country'].value.match(/Nederland/i)) {
      address.removeAttribute('readonly');
      city.removeAttribute('readonly');
      region.removeAttribute('readonly');
      return;
    }

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
      signal: signal
    });
    const json = await response.json();
    if (response.ok && json?.response?.numFound) {
      const record = json.response.docs[0];
      straat.value = record.straatnaam;
      plaats.value = record.woonplaatsnaam;
      provincie.value = record.provincienaam;
      postcodeElement.value = record.postcode;
      return;
    }
    ClientEdit.abortController = null;
  } 
  static async DeleteAttachment(this: HTMLElement){    
    bootbox.confirm({
      message: `<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze bijlage wilt verwijderen?</p><p><strong>LET OP!</strong> dit kan niet ongedaan worden gemaakt.</p></div>`,
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
          Functions.ToastrError('Error', 'Er is een fout opgetreden bij het verwijderen van de klant.');
          return;
        }
        window.location.reload();
      }
    });
  }
  static Initialize() {
    document.getElementById("Country").addEventListener('change', ClientEdit.AddressCheck, false);
    document.getElementById("Zipcode").addEventListener('blur', ClientEdit.AddressCheck, false);
    document.getElementById("Number").addEventListener('blur', ClientEdit.AddressCheck, false);

    ClientEdit.InitForm();
    ClientEdit.InitAttachmentTable();
    document.querySelectorAll('button.btn-danger').forEach(button => button.addEventListener('click', ClientEdit.DeleteAttachment));
  }
  static InitForm() {
    $("#form-client").validate({
      messages: {
        "BoatBrand": "Merk is vereist",
        "BoatType": "Type is vereist",
        "Email": "Email is vereist",
        "LastName": "Achternaam is vereist",
        "Address": "Adres is vereist",
        "City": "Plaats is vereist",
        "Region": "Provincie is vereist",
        "Zipcode": "Postcode is vereist",
        "Number": "Huisnummer is vereist",
      },
      rules: {
        "BoatBrand": "required",
        "BoatType": "required",
        "Email": "required",
        "LastName": "required",
        "Address": "required",
        "City": "required",
        "Region": "required", 
        "Zipcode": "required",
        "Number": "required",
      },
      submitHandler: async function(form, event) {
        event.preventDefault();
        Functions.ShowToastr();
        var data = new FormData(form);
        const uri = new URL(form.action, window.location.origin);
        const response = await fetch(uri.href, {
          body: data,
          method: 'POST',
        });
        if (!response.ok) {
          Functions.RemoveToastr();
          Functions.ToastrError('Error', 'Er is een fout opgetreden bij het opslaan van de order.');
          return;
        }
        window.location.href = '/';
      }
    });
  }
  static InitAttachmentTable() {
    $('#table-attachments').DataTable({
      autoWidth: false,
      columnDefs: [
        { targets: [0, 5], className: "text-center", orderable: false, searchable: false, width: "2rem" }
      ],
      language: {
        url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Dutch.json'
      },
      order: [[1, 'asc']],
      paging: false
    });
  }
}

document.addEventListener('DOMContentLoaded', ClientEdit.Initialize);
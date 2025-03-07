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

    Functions.GetHtmlInputElementById('Address').setAttribute('readonly','readonly');
    Functions.GetHtmlInputElementById('City').setAttribute('readonly','readonly');
    Functions.GetHtmlInputElementById('Region').setAttribute('readonly','readonly');


    if (!elements['Country'].value.match(/Nederland/i)) {
      Functions.GetHtmlInputElementById('Address').removeAttribute('readonly');
      Functions.GetHtmlInputElementById('City').removeAttribute('readonly');
      Functions.GetHtmlInputElementById('Region').removeAttribute('readonly');
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
  
  static Initialize() {
    document.getElementById("Country").addEventListener('change', ClientEdit.AddressCheck, false);
    document.getElementById("Zipcode").addEventListener('blur', ClientEdit.AddressCheck, false);
    document.getElementById("Number").addEventListener('blur', ClientEdit.AddressCheck, false);

    ClientEdit.InitForm();
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
}

document.addEventListener('DOMContentLoaded', ClientEdit.Initialize);
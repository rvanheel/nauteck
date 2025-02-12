class OrderEdit {
  static async AddressCheck(): Promise<void> {
    const form = document.getElementById('form-order') as HTMLFormElement;
    const elements = form.elements;
    const postcodeElement = elements['zipcode'];
    const zipCode: string = postcodeElement.value.trim();
    const number: number = +elements['number'].value;

    elements['address'].value = '';
    elements['city'].value = '';
    elements['region'].value = '';
    if(!elements['country'].value.match(/Nederland/i)) return;

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
  static Initialize() {
    document.getElementById("country").addEventListener('change', OrderEdit.AddressCheck, false);
    document.getElementById("zipcode").addEventListener('blur', OrderEdit.AddressCheck, false);
    document.getElementById("number").addEventListener('blur', OrderEdit.AddressCheck, false);
  }
}
document.addEventListener('DOMContentLoaded', OrderEdit.Initialize);
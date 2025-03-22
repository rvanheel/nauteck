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

    const street = elements['Address'];
    const city = elements['City'];
    const province = elements['Region'];

    street.value = '';
    city.value = '';
    province.value = '';

    const address = Functions.GetHtmlInputElementById('Address');
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
      street.value = record.straatnaam;
      city.value = record.woonplaatsnaam;
      province.value = record.provincienaam;
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
          cache: 'no-cache'
        });
        if (!response.ok) {
          Functions.RemoveToastr();
          Functions.ToastrError('Error', 'Er is een fout opgetreden bij het verwijderen van een attachment.');
          return;
        }
        window.location.href = `${window.location.pathname}#nav-attachments`;
        window.location.reload();
      }
    });
  }
  static async GetSasBlobUrl(extension: string) : Promise<any> {
    const uri = new URL('/Attachment/GetSasBlobUrl', window.location.origin);
    uri.searchParams.append('extension', extension);
    const resp = await fetch(uri.href, { cache: "no-cache", credentials: 'include', method: 'GET' });
    return await resp.json();
  }
  static InitDropZone(){
    const dropzone = document.getElementById('dropzone');

    dropzone.addEventListener('dragover', (event) => {
      event.preventDefault();
    });

    dropzone.addEventListener('dragleave', (event) => {
      event.preventDefault();
    });

    dropzone.addEventListener('drop', async (event) => {
      event.preventDefault();
      const files = event.dataTransfer.files;

      const names = Array.from(files).map(file => file.name).filter(Boolean).join('<br>');

      let pairs = {};
      const rows = Array.from(files).map(file => {
        const id = Functions.UniqueId();
        pairs[id] = file;        
        return `<div data-pair="${id}">${file.name}</div><div>${file.size}</div><div>${file.type}</div><div><div class="progress" role="progressbar" aria-valuemin="0" aria-valuemax="100"><div class="progress-bar" style="width: 0"></div></div></div>`;
      }).filter(Boolean).join('');

      bootbox.confirm({
        message: `<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze bijlage(s) wilt toevoegen?</p>${names}</div>`,
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
          const jumbotron = document.getElementById('dropped');
          jumbotron.insertAdjacentHTML('beforeend', rows);
          
          Functions.ShowToastr();
          let tasks = [];
          Object.getOwnPropertyNames(pairs).forEach(id => tasks.push(ClientEdit.UploadAttachment(id, pairs[id])));
          await Promise.all(tasks);

          window.location.assign(`${window.location.pathname}#nav-attachments`);
          window.location.reload();
        }
      });
    });    

  }
  static Initialize() {
    document.getElementById("Country").addEventListener('change', ClientEdit.AddressCheck, false);
    document.getElementById("Zipcode").addEventListener('blur', ClientEdit.AddressCheck, false);
    document.getElementById("Number").addEventListener('blur', ClientEdit.AddressCheck, false);

    ClientEdit.InitForm();
    ClientEdit.InitAttachmentTable();
    ClientEdit.InitDropZone();
    document.querySelectorAll('button.btn-danger').forEach(button => button.addEventListener('click', ClientEdit.DeleteAttachment));

    const hash = window.location.hash;    
    if (hash) {
      const el = document.querySelector(`button[data-bs-target="${hash}"]`);
      if (el) el.dispatchEvent(new Event('click'));
    }
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
        const data = new FormData(form);
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
        { targets: [0, 5], className: "text-center", orderable: false, searchable: false, width: "2rem" },
        { targets: [2], type: 'num' }
      ],
      language: {
        url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Dutch.json'
      },
      order: [[1, 'asc']],
      paging: false
    });
  }
  static SaveAttachment(blobUri: string, name: string, type: string, size: number): Promise<Response> {
    const id = Functions.GetHtmlInputElementById('Id').value;
    const uri = new URL(`/Attachment/Add`, window.location.origin);
    return fetch(uri.toString(), {
      body: JSON.stringify({ ClientId: id, BlobUri: blobUri, FileName: name, ContentType: type, Size: size }),
      headers: {
        'Content-Type': 'application/json'
      },
      credentials: 'include',
      method: 'POST'
    });
  }
  static UploadAttachment(id: string, file: File) : Promise<void> {
    return new Promise(async (resolve, _) => {
        const uri = await ClientEdit.GetSasBlobUrl(file.name.split('.').pop());
        const b = await file.arrayBuffer();
        await Functions.UploadNewBlob(file.type, uri, b);
        const blobUri = new URL(uri);
        await ClientEdit.SaveAttachment(new URL(blobUri.pathname, blobUri.origin).toString(), file.name, file.type, file.size);
        resolve();
    });
  }
}

document.addEventListener('DOMContentLoaded', ClientEdit.Initialize);
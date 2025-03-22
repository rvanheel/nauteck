class Functions {  
  static FullDateTimeFormat = { timeZone: 'Europe/Amsterdam', dateStyle: 'full', timeStyle: 'medium' } as Intl.DateTimeFormatOptions;
  static Delete(button: HTMLButtonElement, message: string, info: string, errorMessage: string, redirect: string) {
    bootbox.confirm({
      message: `<div class="alert alert-warning"><p class="fw-bold">${message}</p><p><strong>LET OP!</strong> ${info}.</p></div>`,
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
      callback: async (result: boolean) : Promise<void> => {
        if (!result) return;
        Functions.ShowToastr();
        const url = button.dataset.url;
        const response = await fetch(url, {
          method: 'DELETE',
          cache: 'no-cache',
          redirect: 'follow',
        });
        if (!response.ok) {
          Functions.RemoveToastr();
          Functions.ToastrError('Error', errorMessage);
          return;
        }
        window.location.assign(redirect);
        window.location.reload();
      }
    });
  }
  static async GetBlobSasUrl(origin: string, path: string, extension: string) {
    const uri = new URL(path, origin);
    uri.searchParams.append("extension", extension);
    const resp = await fetch(uri.href, { cache: "no-cache" });
    return await resp.json();
  }
  static GetHtmlInputElement(selector: string): HTMLInputElement {
    return document.querySelector(selector) as HTMLInputElement;
  }
  static GetHtmlInputElementById(id: string): HTMLInputElement {
    return document.getElementById(id) as HTMLInputElement;
  }
  static GetHtmlSelectElement(selector: string): HTMLSelectElement {
    return document.querySelector(selector) as HTMLSelectElement;
  }
  static GetHtmlSelectElementById(id: string): HTMLSelectElement {
    return document.getElementById(id) as HTMLSelectElement;
  }  
  static GetSelectOption(selector:string): HTMLOptionElement {
    const select = Functions.GetHtmlSelectElementById(selector);
    return select.options[select.selectedIndex] as HTMLOptionElement;
}
  static RemoveToastr() {
    toastr.clear();
  }
  static SetHtmlTextContent(selector: string, text: string) {
    const element = document.querySelector(selector) as HTMLElement;
    element.textContent = text;
  }
  static ShowToastr(text: string = 'Please wait..', title: string = 'LOADING') {
    toastr.info(text, title, {
      closeButton: false,
      extendedTimeOut: 0,
      hideDuration: 10,
      hideMethod: 'slideUp',
      newestOnTop: true,
      positionClass: 'toast-top-center',
      preventDuplicates: true,
      showDuration: 10,
      showMethod: 'slideDown',
      tapToDismiss: false,
      timeOut: 0
    });
  }

  static ToastrError(title: string, message: string) {
    toastr.error(message, title, {
      hideMethod: 'slideUp',
      hideDuration: 10,
      newestOnTop: true,
      positionClass: 'toast-top-center',
      preventDuplicates: true,
      showDuration: 10,
      showMethod: 'slideDown'
    });
  }
  static UniqueId() {
    return Math.random().toString(36).substring(2, 10);
  }
  static UploadNewBlob(contentType: string, uri: string, body: any): Promise<Response> {
    return fetch(uri, {
      body: body,
      cache: 'no-cache',
      headers: {
        'x-ms-content-type': contentType,
        'x-ms-blob-type': 'BlockBlob',
        'x-ms-version': '2020-10-02',
      },
      method: 'PUT',
    });
  }
}
export default Functions;
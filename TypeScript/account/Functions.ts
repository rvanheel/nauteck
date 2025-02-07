class Functions {
  static FullDateTimeFormat = { timeZone: 'Europe/Amsterdam', dateStyle: 'full', timeStyle: 'medium' } as Intl.DateTimeFormatOptions;
  
  static async GetBlobSasUrl(origin: string, path: string, extension: string) {
    const uri = new URL(path, origin);
    uri.searchParams.append("extension", extension);
    const resp = await fetch(uri.href, { cache: "no-cache" });
    return await resp.json();
  }
  
  static RemoveToastr() {
    toastr.remove();
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
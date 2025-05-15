class InvoiceIndex {
  static InitDataTable(){
    $("#table-quotations").DataTable({
      autoWidth: false,
      columnDefs:[
          { targets: [0,7], className: 'text-center', width: '5rem'},
          { targets: [1,2], className: 'text-center', width: '8rem'},
          { targets: [3,6], type: 'num', width: '10rem'}
      ],
      order: [[3, 'desc']],
      stateSave: true
  });
  }
  static Initialize(){
    InvoiceIndex.InitDataTable();
    document.getElementById("Status").addEventListener("change", InvoiceIndex.StatusChange);
  }
  static StatusChange(this: HTMLSelectElement){
    const url = new URL(window.location.pathname, window.location.origin);
    url.searchParams.append("status", this.value);
    window.location.replace(url);
  }
}
document.addEventListener("DOMContentLoaded", InvoiceIndex.Initialize);
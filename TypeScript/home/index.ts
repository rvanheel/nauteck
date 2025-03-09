import Functions from "../account/Functions";

class HomeIndex {
  static async Delete(this: HTMLElement) {
    const id = window.location.pathname.split('/').pop();
    bootbox.confirm({
      message: `<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze klant wilt verwijderen?</p><p><strong>LET OP!</strong> dit kan niet ongedaan worden gemaakt en alle gekoppelde items en documenten worden tevens verwijderd.</p></div>`,
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
        window.location.replace('/');
      }
    });
  }
  static InitDatatable() {
    $('#table-orders').DataTable({
      autoWidth: false,
      columnDefs: [
        { targets: [0, 10], className: "text-center", orderable: false, searchable: false, width: "2rem" }
      ],
      order: [[1, 'asc'], [3, 'asc'], [4, 'asc']],
      deferRender: true,
      language: {
        url: 'https://cdn.datatables.net/plug-ins/1.10.25/i18n/Dutch.json'
      },
      lengthMenu: [10, 25, 50, 100],
      pageLength: 10,
      processing: true,
      stateSave: true
    });
  }
  static Initialize() {
    document.querySelectorAll('button.btn-danger').forEach(button => button.addEventListener('click', HomeIndex.Delete));
    HomeIndex.InitDatatable();
  }
}
document.addEventListener('DOMContentLoaded', HomeIndex.Initialize);
import Functions from '../Functions';

class HomeIndex {
  static async Delete(this: HTMLButtonElement) {
    Functions.Delete(this,
        'Weet u zeker dat u deze klant wilt verwijderen?',
        'dit kan niet ongedaan worden gemaakt en alle gekoppelde items en documenten worden tevens verwijderd.',
        'Er is een fout opgetreden bij het verwijderen van een klant.',
        window.location.pathname);
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

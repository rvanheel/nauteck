import Functions from "../account/Functions";

class StatusIndex {
    static Delete(this: HTMLButtonElement) {
        Functions.Delete(this,
            'Weet u zeker dat u deze status wilt verwijderen?',
            'dit kan niet ongedaan worden gemaakt.',
            'Er is een fout opgetreden bij het verwijderen van een status.',
            window.location.pathname);
    }
    static InitDataTable() {
        $("#table-status").DataTable({
            autoWidth: false,
            columnDefs:[
                { targets: [0, 4, 5], className: "text-center", orderable: false, searchable: false, width: '5rem' },
                { targets: [2,3], width: '15rem' }
            ],
            order: [[1, 'asc']],
            deferRender: true,
            processing: true,
            stateSave: true
        });
    }
    static Initialize() {
        document.querySelectorAll('button[data-type="delete"]').forEach(button => button.addEventListener('click', StatusIndex.Delete));
        StatusIndex.InitDataTable();
    }
}
document.addEventListener('DOMContentLoaded', StatusIndex.Initialize);
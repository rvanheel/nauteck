import Functions from "./Functions";

class AccountIndex {
  static async Delete(this: HTMLElement) {
    const id = window.location.pathname.split('/').pop();
    bootbox.confirm({
      message: `<div class="alert alert-warning"><p class="fw-bold">Weet u zeker dat u deze gebruiker wilt verwijderen?</p><p><strong>LET OP!</strong> dit kan niet ongedaan worden gemaakt.</p></div>`,
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
          Functions.ToastrError('Error', 'Er is een fout opgetreden bij het verwijderen van de order.');
          return;
        }
        window.location.reload();
      }
    });
  }
  static Initialize() {
    document.querySelectorAll('button[data-type="delete"]').forEach(button => button.addEventListener('click', AccountIndex.Delete));
  }
}
document.addEventListener('DOMContentLoaded', AccountIndex.Initialize);
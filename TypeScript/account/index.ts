import Functions from "./Functions";

class AccountIndex {
  static async Delete(this: HTMLButtonElement) {
    Functions.Delete(this,
        'Weet u zeker dat u deze gebruiker wilt verwijderen?',
        'dit kan niet ongedaan worden gemaakt.',
        'Er is een fout opgetreden bij het verwijderen van een order.',
        window.location.pathname);
  }
  static Initialize() {
    document.querySelectorAll('button[data-type="delete"]').forEach(button => button.addEventListener('click', AccountIndex.Delete));
  }
}
document.addEventListener('DOMContentLoaded', AccountIndex.Initialize);
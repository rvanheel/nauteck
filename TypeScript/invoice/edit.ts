import Functions from '../Functions';

class InvoiceEdit {
	static AddColor() {
		Functions.ShowModalLine("#modal-invoice-color", "form-color-line");
	}
	static AddFloor() {
		Functions.ShowModalLine("#modal-invoice-floor", "form-floor-line");
	}
	static AddLine() {
		Functions.ShowModalLine("#modal-invoice-line", "form-invoice-line");
	}
	static EditLine(this: HTMLElement){
		const button = this as HTMLButtonElement;
		const modal = $("#modal-invoice-line");
		const form = document.getElementById("form-invoice-line") as HTMLFormElement;
		form.reset();

		(form.elements.namedItem("Id") as HTMLInputElement).value = button.dataset.id;
		(form.elements.namedItem("Amount") as HTMLInputElement).value = button.dataset.amount;
		(form.elements.namedItem("Description") as HTMLInputElement).value = button.dataset.description;
		(form.elements.namedItem("Tax") as HTMLSelectElement).value = button.dataset.tax;
		(form.elements.namedItem("Quantity") as HTMLInputElement).value = button.dataset.quantity;

		modal.modal("show");
	}
	static Initialize() {
		["#form-invoice-line", "#form-floor-line", "#form-color-line"].forEach(Functions.InitInvoiceOrQuotationFormLine);

		document.getElementById("button-add-color").addEventListener("click", InvoiceEdit.AddColor);
		document.getElementById("button-add-floor").addEventListener("click", InvoiceEdit.AddFloor);
		document.getElementById("button-add-line").addEventListener("click", InvoiceEdit.AddLine);
		document.querySelectorAll("button[data-type='edit']").forEach(button => button.addEventListener("click", InvoiceEdit.EditLine));

		["Omschrijving_1", "Omschrijving_2"].forEach(xid => Functions.GetHtmlSelectElementById(xid).addEventListener("change", Functions.FloorDescriptionChange));
	}
}
document.addEventListener("DOMContentLoaded", InvoiceEdit.Initialize);

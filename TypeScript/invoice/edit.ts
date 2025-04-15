class InvoiceEdit {
	static AddLine() {
		const modal = $("#modal-invoice-line");
		const form = document.getElementById("form-invoice-line") as HTMLFormElement;
		form.reset();

		(form.elements.namedItem("Id") as HTMLInputElement).value = '00000000-0000-0000-0000-000000000000';

		modal.on("show.bs.modal", function () {
			setTimeout(() => {
				(form.elements.namedItem("Quantity") as HTMLInputElement).focus();
			}, 250);
		});

		modal.modal("show");
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
		InvoiceEdit.InitForm("#form-invoice-line");
		document.getElementById("button-add-line").addEventListener("click", InvoiceEdit.AddLine);
		document.querySelectorAll("button[data-type='edit']").forEach(button => button.addEventListener("click", InvoiceEdit.EditLine));
	}
	static InitForm(identifier: string) {
		$(identifier).validate({
			messages: {
				Amount: "Bedrag is vereist",
				Description: "Omschrijving is vereist",
				Quantity: "Aantal is vereist"
			},
			rules: {
				Amount: {
					required: true,
					number: true
				},
				Description: {
					required: true
				},
				Quantity: {
					required: true,
					number: true
				}
			}
		});
	}

}
document.addEventListener("DOMContentLoaded", InvoiceEdit.Initialize);

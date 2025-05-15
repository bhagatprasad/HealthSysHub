import { Directive, Input } from '@angular/core';
import { NgForm } from '@angular/forms';

@Directive({
  selector: '[appFormValidator]',
  standalone: true
})
export class FormValidatorDirective {
  @Input() form!: NgForm;  // Changed from appFormValidator to form
  @Input() submitButton!: HTMLButtonElement;

  ngAfterViewInit() {
    if (this.form && this.submitButton) {
      this.form.valueChanges?.subscribe(() => {
        this.updateButtonState();
      });
      this.updateButtonState();
    }
  }

  private updateButtonState() {
    const isValid = this.form.valid;
    this.submitButton.disabled = !isValid;
  }
}
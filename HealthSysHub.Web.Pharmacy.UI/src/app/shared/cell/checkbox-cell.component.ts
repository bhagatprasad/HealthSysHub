// checkbox-column.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-checkbox-column',
  template: `
    <input 
      type="checkbox" 
      [checked]="checked" 
      (change)="onChange($event)"
    >
  `,
  styles: [`
    input[type="checkbox"] {
      margin: 0;
      cursor: pointer;
    }
  `]
})
export class CheckboxColumnComponent {
  @Input() checked: boolean = false;
  @Output() changed = new EventEmitter<boolean>();

  onChange(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.changed.emit(isChecked);
  }
}
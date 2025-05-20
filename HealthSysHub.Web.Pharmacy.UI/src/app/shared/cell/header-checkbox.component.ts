// header-checkbox.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-header-checkbox',
  template: `
    <input 
      type="checkbox" 
      [checked]="allSelected" 
      [indeterminate]="someSelected"
      (change)="toggleAll($event)"
    >
  `,
  styles: [`
    input[type="checkbox"] {
      margin: 0;
      cursor: pointer;
    }
  `]
})
export class HeaderCheckboxComponent {
  @Input() allSelected: boolean = false;
  @Input() someSelected: boolean = false;
  @Output() changed = new EventEmitter<boolean>();

  toggleAll(event: Event) {
    const isChecked = (event.target as HTMLInputElement).checked;
    this.changed.emit(isChecked);
  }
}
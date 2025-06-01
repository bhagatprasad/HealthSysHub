import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
    selector: 'app-paynow',
    standalone: true,
    imports: [],
    templateUrl: './paynow.component.html',
    styleUrl: './paynow.component.css'
})
export class PaynowComponent {
    @Input() isOpen = false;
    @Input() title = '';
    @Input() message = '';

    @Output() confirmed = new EventEmitter<string>();
    @Output() closed = new EventEmitter<void>();

    notes = '';

    confirm() {
        this.confirmed.emit(this.notes);
        this.isOpen = false;
    }

    close() {
        this.closed.emit();
        this.isOpen = false;
    }
}
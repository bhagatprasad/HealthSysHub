import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-modal',
    standalone: true,
    imports: [ReactiveFormsModule, CommonModule, FormsModule],
    templateUrl: './modal.component.html',
    styleUrls: ['./modal.component.css'],
})
export class ModalComponent {
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
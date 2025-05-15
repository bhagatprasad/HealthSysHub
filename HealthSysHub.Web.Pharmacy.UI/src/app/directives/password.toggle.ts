import { Directive, ElementRef, Renderer2, HostListener, OnInit } from '@angular/core';

@Directive({
    selector: '[appPasswordToggle]'
})
export class PasswordToggleDirective implements OnInit {
    private toggleButton!: HTMLElement;
    private shown = false;

    constructor(
        private el: ElementRef,
        private renderer: Renderer2
    ) { }

    ngOnInit(): void {
        this.createToggleButton();
    }

    private createToggleButton(): void {
        const parent = this.renderer.parentNode(this.el.nativeElement);
        if (!parent) return;

        this.renderer.setStyle(parent, 'position', 'relative');
        this.renderer.setStyle(this.el.nativeElement, 'padding-right', '2.5rem');

        this.toggleButton = this.renderer.createElement('i');

        // Set all attributes and styles
        this.setToggleButtonStyles();
        this.setToggleButtonAttributes();

        this.renderer.appendChild(parent, this.toggleButton);
    }

    private setToggleButtonStyles(): void {
        const styles = {
            'position': 'absolute',
            'top': '50%',
            'right': '0.75rem',
            'transform': 'translateY(-50%)',
            'font-size': '1.25rem',
            'color': '#6c757d',
            'cursor': 'pointer'
        };

        Object.entries(styles).forEach(([key, value]) => {
            this.renderer.setStyle(this.toggleButton, key, value);
        });
    }

    private setToggleButtonAttributes(): void {
        const attributes = {
            'class': 'mdi mdi-eye-off',
            'aria-label': 'Toggle password visibility',
            'tabindex': '0',
            'role': 'button',
            'title': 'Show/Hide password'
        };

        Object.entries(attributes).forEach(([key, value]) => {
            if (key === 'class') {
                value.split(' ').forEach(className => {
                    this.renderer.addClass(this.toggleButton, className);
                });
            } else {
                this.renderer.setAttribute(this.toggleButton, key, value);
            }
        });
    }

    @HostListener('document:click', ['$event'])
    handleClick(event: MouseEvent): void {
        if (!this.toggleButton) return;

        const clickedIcon = event.target === this.toggleButton;
        const clickedIconChild = this.toggleButton.contains(event.target as Node);

        if (clickedIcon || clickedIconChild) {
            event.preventDefault();
            this.toggle();
        }
    }

    private toggle(): void {
        this.shown = !this.shown;
        const type = this.shown ? 'text' : 'password';
        const iconClass = this.shown ? 'mdi-eye' : 'mdi-eye-off';
        const removeClass = this.shown ? 'mdi-eye-off' : 'mdi-eye';

        this.renderer.setAttribute(this.el.nativeElement, 'type', type);
        this.renderer.removeClass(this.toggleButton, removeClass);
        this.renderer.addClass(this.toggleButton, iconClass);
    }
}
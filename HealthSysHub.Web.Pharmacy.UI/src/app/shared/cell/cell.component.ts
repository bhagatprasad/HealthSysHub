import { CurrencyPipe } from "@angular/common";
import { Component, Input } from "@angular/core";
import { ColumnDataSchemaModel } from "@revolist/angular-datagrid";

@Component({
    selector: "app-active-cell",
    standalone: true,
    template: `
    <span [style.color]="value ? 'green' : 'red'">
      {{ value ? '✔' : '✖' }}
    </span>
  `,
})
export class ActiveCellComponent {
    @Input() props!: ColumnDataSchemaModel;

    get value() {
        return this.props.model[this.props.prop];
    }
}

@Component({
    selector: "app-price-cell",
    standalone: true,
    template: `{{ value}}`,
})
export class PriceCellComponent {
    @Input() props!: ColumnDataSchemaModel;

    get value() {
        return this.props.model[this.props.prop] || 0;
    }
}
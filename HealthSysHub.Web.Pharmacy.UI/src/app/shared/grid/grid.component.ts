import { Component, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { GridAction } from './grid.action';
import { GridColumn } from './grid.column';




@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.css']
})
export class GridComponent implements OnChanges {
  @Input() columns: GridColumn[] = [];
  @Input() data: any[] = [];
  @Input() actions: GridAction[] = [];
  @Input() pageSize: number = 10;
  @Input() totalRecords: number = 0;

  @Output() actionClicked = new EventEmitter<{action: string, data: any}>();
  @Output() sortChanged = new EventEmitter<{field: string, direction: 'asc' | 'desc'}>();
  @Output() searchChanged = new EventEmitter<string>();
  @Output() selectionChanged = new EventEmitter<any[]>();

  filteredData: any[] = [];
  selectedItems: any[] = [];
  allSelected: boolean = false;
  searchText: string = '';
  sortField: string = '';
  sortDirection: 'asc' | 'desc' = 'asc';

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['data']) {
      this.filteredData = [...this.data];
      this.updateSelectionState();
    }
  }

  onSearch(): void {
    this.searchChanged.emit(this.searchText);
  }

  onSort(field: string): void {
    if (!this.columns.find(c => c.field === field)?.sortable) return;
    
    if (this.sortField === field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = field;
      this.sortDirection = 'asc';
    }
    
    this.sortChanged.emit({field: this.sortField, direction: this.sortDirection});
  }

  toggleSelectAll(): void {
    this.allSelected = !this.allSelected;
    this.filteredData.forEach(item => item.selected = this.allSelected);
    this.updateSelectedItems();
  }

  toggleSelectItem(item: any): void {
    item.selected = !item.selected;
    this.updateSelectedItems();
  }

  private updateSelectedItems(): void {
    this.selectedItems = this.filteredData.filter(item => item.selected);
    this.allSelected = this.selectedItems.length === this.filteredData.length && this.filteredData.length > 0;
    this.selectionChanged.emit(this.selectedItems);
  }

  private updateSelectionState(): void {
    const selectedIds = new Set(this.selectedItems.map(item => item.id));
    this.filteredData.forEach(item => {
      item.selected = selectedIds.has(item.id);
    });
    this.allSelected = this.selectedItems.length === this.filteredData.length && this.filteredData.length > 0;
  }

  onActionClick(action: string): void {
    this.actionClicked.emit({action, data: this.selectedItems});
  }

  isActionDisabled(action: GridAction): boolean {
    if (action.singleSelectOnly && this.selectedItems.length !== 1) return true;
    if (action.multiSelectOnly && this.selectedItems.length < 1) return true;
    return false;
  }

  getProgressBarClass(percentage: number): string {
    if (percentage < 30) return 'bg-danger';
    if (percentage < 60) return 'bg-warning';
    if (percentage < 90) return 'bg-info';
    return 'bg-success';
  }
}
export interface GridColumn {
  header: string;
  field: string;
  sortable?: boolean;
  type?: 'text' | 'image' | 'progress' | 'date' | 'currency';
  width?: string;
}

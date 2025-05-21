export interface GridAction {
  name: string;
  icon: string;
  tooltip: string;
  singleSelectOnly?: boolean;
  multiSelectOnly?: boolean;
  color?: string;
}
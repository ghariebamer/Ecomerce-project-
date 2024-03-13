import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrl: './page-header.component.css'
})
export class PageHeaderComponent {
  @Input() totalCount!:number;
  @Input() PageNumber!:number;
  @Input() PageSize!:number;

}
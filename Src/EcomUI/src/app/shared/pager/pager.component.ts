import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrl: './pager.component.css'
})
export class PagerComponent {
@Input() totalItems!:number
@Input() itemsPerPage!:number
@Output() pageChaged= new EventEmitter<number>();

constructor() {
}


OnPageChanger(event:any){
  this.pageChaged.emit(event);
}

}

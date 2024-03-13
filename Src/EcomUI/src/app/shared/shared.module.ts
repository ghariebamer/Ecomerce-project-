import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {HttpClient,HttpClientModule} from '@angular/common/http'
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PageHeaderComponent } from './page-header/page-header.component';
import { PagerComponent } from './pager/pager.component';



@NgModule({
  declarations: [
    PageHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    PaginationModule.forRoot()
  ],
  exports:[PaginationModule,PageHeaderComponent,PagerComponent]
})
export class SharedModule { }

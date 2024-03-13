import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from '../../interfaces/Models/Products';
import { Pagination } from '../../interfaces/pagination';
import { CategoryService } from '../category.service';
import { Category } from '../../interfaces/Models/Category';
import { ShopParams } from '../../interfaces/Models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.css'
})
export class ShopComponent implements OnInit {
  @ViewChild('searchEl') searchElement?:ElementRef;
Products:Product[]=[];
Categories:Category[]=[];
selectOptions=[
  {name:'Name',value:''},
  {name:'Price:Min-max',value:'PriceAsc'},
  {name:'Price:Max-min',value:'PriceDesc'}
]
ShopParams=<ShopParams>{PageNumber:1,PageSize:2,Search:''}
pageResponseModel=<Pagination>{}
constructor(private _shopservice:ShopService,private _CategoryService:CategoryService) {

}
  ngOnInit(){
this.GetAllProduct();
this.GetAllCategories();
  }

GetAllProduct(shopParams?:ShopParams){
      this._shopservice.getproducts(shopParams).subscribe((data:Pagination)=>{
        if(data){
          this.pageResponseModel.totalCount=data.totalCount;
          this.pageResponseModel._List=data._List;
          this.pageResponseModel.pageNumber=data.pageNumber;
          this.pageResponseModel.pageSize=data.pageSize;
        }
      } )
}
GetAllCategories(){
  this._CategoryService.getAllCategories().subscribe((res:Category[])=>{
    if(res){
      this.Categories=[{id:0,name:'All',descrption:''},...res];
    }
  })
}
selectCategory(cat:number){
  (cat &&cat>0)? this.ShopParams.CategoryId=cat:this.ShopParams.CategoryId=null 
    this.GetAllProduct(this.ShopParams);
    this.ShopParams=<ShopParams>{PageNumber:0,PageSize:2,Search:''}
}
OnSortingproducts(sort:Event){
  let sortvalue=(sort.target as HTMLInputElement).value;
  this.ShopParams.Sort=sortvalue;
  this.GetAllProduct(this.ShopParams)
}

OnPageChanged(page:any){  
  this.ShopParams.PageNumber=page;
  this.GetAllProduct(this.ShopParams)
}

onSearch(){
  if(this.searchElement)
  this.ShopParams.Search=this.searchElement.nativeElement.value;
  console.log("🚀 ~ ShopComponent ~ onSearch ~ this.ShopParams.Search:", this.ShopParams.Search)
this.GetAllProduct(this.ShopParams)
}

onRest(){
if(this.searchElement)
this.searchElement.nativeElement.value=''
this.ShopParams.Search=''
this.GetAllProduct(this.ShopParams)
}

}

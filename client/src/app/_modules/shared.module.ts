import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ToastrModule.forRoot({ positionClass: 'toast-bottom-right' }),
    BsDropdownModule.forRoot(),
    PaginationModule.forRoot(),
  ],
  exports: [BsDropdownModule, ToastrModule, PaginationModule],
})
export class SharedModule { }

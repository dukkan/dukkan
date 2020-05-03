import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HttpClientJsonpModule } from '@angular/common/http';
import { AbpModule } from 'abp-ng2-module/dist/src/abp.module';
import { SharedModule } from '@shared/shared.module';
import { ServiceProxyModule } from '@shared/service-proxies/service-proxy.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { CategoryRoutingModule } from './category-routing.module';
import { CategoriesComponent } from './categories.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { TableModule } from 'primeng/components/table/table';
import { PaginatorModule } from 'primeng/components/paginator/paginator';
import { CategoryAddOrEditModalComponent } from './category-add-or-edit-modal.component';
import { TabsModule } from 'ngx-bootstrap/tabs';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    HttpClientJsonpModule,
    AbpModule,
    SharedModule,
    ServiceProxyModule,
    ModalModule.forChild(),
    BsDropdownModule,
    TabsModule,
    NgxPaginationModule,
    TableModule,
    PaginatorModule,
    CategoryRoutingModule,
  ],
  declarations: [CategoriesComponent, CategoryAddOrEditModalComponent],
  entryComponents: [CategoryAddOrEditModalComponent],
})
export class CategoryModule {}

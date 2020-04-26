import { Component, Injector, ViewChild } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  CategoryServiceProxy,
  CategoryListDto,
} from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef, ModalOptions } from 'ngx-bootstrap/modal';
import { CategoryAddOrEditModalComponent } from './category-add-or-edit-modal.component';

@Component({
  templateUrl: './categories.component.html',
  animations: [appModuleAnimation()],
})
export class CategoriesComponent extends AppComponentBase {
  @ViewChild('dataTable', { static: true }) dataTable: Table;
  @ViewChild('paginator', { static: true }) paginator: Paginator;

  keyword = '';
  advancedFiltersVisible = false;

  constructor(
    injector: Injector,
    private _categoryService: CategoryServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  getRecords(event?: LazyLoadEvent): void {
    if (this.primengTableHelper.shouldResetPaging(event)) {
      this.paginator.changePage(0);
      return;
    }

    this.primengTableHelper.showLoadingIndicator();

    this._categoryService
      .getAllPaged(
        this.keyword,
        this.primengTableHelper.getSorting(this.dataTable),
        this.primengTableHelper.getSkipCount(this.paginator, event),
        this.primengTableHelper.getMaxResultCount(this.paginator, event)
      )
      .pipe(finalize(() => this.primengTableHelper.hideLoadingIndicator()))
      .subscribe((result) => {
        this.primengTableHelper.totalRecordsCount = result.totalCount;
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.hideLoadingIndicator();
      });
  }

  reloadPage(): void {
    this.paginator.changePage(this.paginator.getPage());
  }

  showAddModal(): void {
    this.showAddOrEditModal();
  }

  showEditModal(listDto: CategoryListDto): void {
    this.showAddOrEditModal(listDto.id);
  }

  showAddOrEditModal(id?: number): void {
    let config: ModalOptions = {
      class: 'modal-xl',
    };

    if (id) {
      config.initialState = {
        id: id,
      };
    }

    const modal = this._modalService.show(
      CategoryAddOrEditModalComponent,
      config
    );
    modal.content.onSave.subscribe(() => {
      this.getRecords();
    });
  }

  deleteRecord(listDto: CategoryListDto): void {
    abp.message.confirm(
      this.l('AreYouSure', listDto.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._categoryService.remove(listDto.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.getRecords();
          });
        }
      }
    );
  }
}

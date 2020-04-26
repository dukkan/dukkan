import {
  Component,
  Injector,
  OnInit,
  Output,
  EventEmitter,
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  CategoryServiceProxy,
  CategoryEditDto,
} from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';

@Component({
  templateUrl: 'category-add-or-edit-modal.component.html',
})
export class CategoryAddOrEditModalComponent extends AppComponentBase
  implements OnInit {
  id: number;
  saving = false;
  editDto = new CategoryEditDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _categoryService: CategoryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.id) {
      this._categoryService
        .getForEdit(this.id)
        .subscribe((result: CategoryEditDto) => {
          this.editDto = result;
        });
    } else {
      //! required for add mode
      this.editDto.translations = [];
    }
  }

  save(): void {
    this.saving = true;

    this._categoryService
      .addOrEdit(this.editDto)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }
}

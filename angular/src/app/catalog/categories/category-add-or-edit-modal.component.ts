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
  CategoryTranslationEditDto,
} from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';
import { MultiLingualModelService } from '@shared/components/translation-editor/multi-lingual-model.service';

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
    public bsModalRef: BsModalRef,
    private _categoryService: CategoryServiceProxy,
    private _multiLingualModelService: MultiLingualModelService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    if (this.id) {
      this._categoryService
        .getForEdit(this.id)
        .subscribe((result: CategoryEditDto) => {
          this.editDto = result;
          this.prepareTranslationModels(true);
        });
    } else {
      this.prepareTranslationModels();
    }
  }

  prepareTranslationModels(editMode = false): void {
    if (!editMode) {
      this.editDto.translations = this._multiLingualModelService.prepareTranslationModels(
        CategoryTranslationEditDto
      );
    }

    let translationConfigurer = (translation: CategoryTranslationEditDto) => {
      var existingTranslation = _.find(
        this.editDto.translations,
        (x) => x.language === translation.language
      );

      if (existingTranslation) {
        translation.name = existingTranslation.name;
        translation.description = existingTranslation.description;
      }

      return translation;
    };

    this.editDto.translations = this._multiLingualModelService.prepareTranslationModels(
      CategoryTranslationEditDto,
      translationConfigurer
    );
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

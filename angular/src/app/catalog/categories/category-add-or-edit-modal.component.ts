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
  CategoryListDto,
} from '@shared/service-proxies/service-proxies';
import * as _ from 'lodash';
import { MultiLingualEditorService } from '@shared/components/multi-lingual-editor/multi-lingual-editor.service';

@Component({
  templateUrl: 'category-add-or-edit-modal.component.html',
})
export class CategoryAddOrEditModalComponent
  extends AppComponentBase
  implements OnInit {
  id: number;
  saving = false;
  editDto = new CategoryEditDto();
  allCategories: CategoryListDto[];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public bsModalRef: BsModalRef,
    private _categoryService: CategoryServiceProxy,
    private _multiLingualEditorService: MultiLingualEditorService
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.prepareParentCategories();

    if (this.id) {
      this._categoryService
        .getForEdit(this.id)
        .subscribe((result: CategoryEditDto) => {
          this.editDto = result;
          this.prepareTranslationModels();
        });
    } else {
      this.prepareTranslationModels();
    }
  }

  prepareParentCategories(): void {
    this._categoryService.getAll().subscribe((result) => {
      this.allCategories = result.items;
      this.editDto.parentCategoryId = this.allCategories[0].id;
    });
  }

  prepareTranslationModels(): void {
    if (!this.editDto.id) {
      this.editDto.translations = this._multiLingualEditorService.prepareTranslationModels(
        CategoryTranslationEditDto
      );

      return;
    }

    let translationConfigurer = (translation: CategoryTranslationEditDto) => {
      const existingTranslation = _.find(
        this.editDto.translations,
        (x) => x.language === translation.language
      );

      if (existingTranslation) {
        translation.init(existingTranslation);
      }

      return translation;
    };

    this.editDto.translations = this._multiLingualEditorService.prepareTranslationModels(
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

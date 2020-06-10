import {
  Component,
  ContentChild,
  Input,
  Directive,
  TemplateRef,
  OnInit,
} from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import * as _ from 'lodash';
import { IModelTranslation } from './multi-lingual.models';
import { MultiLingualModelService } from './multi-lingual-model.service';

@Directive({
  selector: '[multiLingualEditorTranslation]',
})
export class MultiLingualEditorTranslationDirective {
  constructor(public template: TemplateRef<any>) {}
}

@Component({
  selector: 'multi-lingual-editor',
  templateUrl: './multi-lingual-editor.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
})
export class MultiLingualEditorComponent implements OnInit {
  @ContentChild(MultiLingualEditorTranslationDirective, { static: false })
  translationDirective: MultiLingualEditorTranslationDirective;

  @Input() translations: IModelTranslation[];

  languages = this._multiLingualModelService.getSortedLanguages();
  defaultLanguage = this._multiLingualModelService.getDefaultLanguage(
    this.languages
  );
  currentLanguage = abp.localization.currentLanguage;
  languageByNameMap: { [key: string]: abp.localization.ILanguageInfo } = {};

  constructor(private _multiLingualModelService: MultiLingualModelService) {}

  ngOnInit(): void {
    this.bindLanguageByNameMap();
  }

  private bindLanguageByNameMap(): void {
    _.forEach(this.languages, (language) => {
      this.languageByNameMap[language.name] = language;
    });
  }
}

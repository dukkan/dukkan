import { Component, ContentChild, Input, OnInit } from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import * as _ from 'lodash';
import { IModelTranslation } from './multi-lingual.model';
import { MultiLingualEditorService } from './multi-lingual-editor.service';
import { MultiLingualEditorTranslationDirective } from './multi-lingual-editor-translation.directive';

@Component({
  selector: 'multi-lingual-editor',
  templateUrl: './multi-lingual-editor.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
})
export class MultiLingualEditorComponent implements OnInit {
  @ContentChild(MultiLingualEditorTranslationDirective, { static: false })
  translationDirective: MultiLingualEditorTranslationDirective;

  @Input() translations: IModelTranslation[];

  languages: abp.localization.ILanguageInfo[] = this._multiLingualEditorService.getAllLanguages();
  currentLanguage = abp.localization.currentLanguage;
  defaultLanguage: abp.localization.ILanguageInfo = this._multiLingualEditorService.getDefaultLanguage();
  languagesByName: { [key: string]: abp.localization.ILanguageInfo } = {};

  constructor(private _multiLingualEditorService: MultiLingualEditorService) {}

  ngOnInit(): void {
    this.bindLanguageByNameMap();
  }

  private bindLanguageByNameMap(): void {
    _.forEach(this.languages, (language) => {
      this.languagesByName[language.name] = language;
    });
  }
}

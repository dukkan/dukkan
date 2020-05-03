import {
  Component,
  ContentChild,
  Input,
  Directive,
  TemplateRef,
} from '@angular/core';
import { ControlContainer, NgForm } from '@angular/forms';
import * as _ from 'lodash';
import { IModelTranslation } from './multi-lingual.models';

@Directive({
  selector: '[multiLingualEditorDefault]',
})
export class MultiLingualEditorDefaultDirective {
  public readonly defaultIndex = 0;
  constructor(public template: TemplateRef<any>) {}
}

@Directive({
  selector: '[multiLingualEditorTranslations]',
})
export class MultiLingualEditorTranslationsDirective {
  constructor(public template: TemplateRef<any>) {}
}

@Component({
  selector: 'multi-lingual-editor',
  templateUrl: './multi-lingual-editor.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: NgForm }],
})
export class MultiLingualEditorComponent {
  @ContentChild(MultiLingualEditorDefaultDirective, { static: false })
  defaultDirective: MultiLingualEditorDefaultDirective;

  @ContentChild(MultiLingualEditorTranslationsDirective, { static: false })
  translationsDirective: MultiLingualEditorTranslationsDirective;

  @Input() translations: IModelTranslation[];

  languageByNameMap: { [key: string]: abp.localization.ILanguageInfo } = {};

  constructor() {
    this.bindLanguageByNameMap();
  }

  private bindLanguageByNameMap(): void {
    const currentLanguage = abp.localization.currentLanguage;
    this.languageByNameMap[currentLanguage.name] = currentLanguage;

    _.forEach(abp.localization.languages, (language) => {
      if (!language.isDisabled && language.name !== currentLanguage.name) {
        this.languageByNameMap[language.name] = language;
      }
    });
  }
}

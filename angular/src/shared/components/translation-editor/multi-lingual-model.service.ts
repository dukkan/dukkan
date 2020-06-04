import { Injectable } from '@angular/core';
import { ITranslationModel } from './multi-lingual.models';
import * as _ from 'lodash';

@Injectable()
export class MultiLingualModelService {
  languages: abp.localization.ILanguageInfo[] = [];

  constructor() {
    this.bindLanguages();
  }

  private bindLanguages(): void {
    const defaultLanguage = _.find(
      abp.localization.languages,
      (x) => !x.isDisabled && x.isDefault
    );
    this.languages = [defaultLanguage];

    _.forEach(abp.localization.languages, (language) => {
      if (!language.isDisabled && language.name !== defaultLanguage.name) {
        this.languages.push(language);
      }
    });
  }

  prepareTranslationModels<TTranslation extends ITranslationModel>(
    translationType: { new (...args: any[]): TTranslation },
    configurer: (translation: TTranslation) => TTranslation = null
  ): TTranslation[] {
    const translations: TTranslation[] = [];

    _.forEach(this.languages, (language) => {
      const translation = new translationType();
      translation.language = language.name;

      if (configurer) {
        configurer.call(this, translation);
      }

      translations.push(translation);
    });

    return translations;
  }
}

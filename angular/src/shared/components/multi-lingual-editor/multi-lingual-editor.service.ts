import { Injectable } from '@angular/core';
import * as _ from 'lodash';
import { IModelTranslation } from './multi-lingual.model';

@Injectable()
export class MultiLingualEditorService {
  getDefaultLanguage(): abp.localization.ILanguageInfo {
    return _.find(abp.localization.languages, (x) => x.isDefault);
  }

  getAllLanguages(): abp.localization.ILanguageInfo[] {
    const currentLanguage = abp.localization.currentLanguage;
    const languages = [currentLanguage];

    const defaultLanguage = this.getDefaultLanguage();
    if (defaultLanguage.name != currentLanguage.name) {
      languages.push(defaultLanguage);
    }

    const languagesExceptCurrentAndDefaultLanguage = _.filter(
      abp.localization.languages,
      (x) => x.name != currentLanguage.name && x.name != defaultLanguage.name
    );

    if (languagesExceptCurrentAndDefaultLanguage) {
      languages.push(...languagesExceptCurrentAndDefaultLanguage);
    }

    return languages;
  }

  prepareTranslationModels<TTranslation extends IModelTranslation>(
    translationType: { new (...args: any[]): TTranslation },
    configurer: (translation: TTranslation) => TTranslation = null
  ): TTranslation[] {
    const translations: TTranslation[] = [];

    _.forEach(this.getAllLanguages(), (language) => {
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

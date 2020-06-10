import { Injectable } from '@angular/core';
import { IModelTranslation } from './multi-lingual.models';
import * as _ from 'lodash';

@Injectable()
export class MultiLingualModelService {
  private languages: abp.localization.ILanguageInfo[] = [];

  constructor() {
    this.languages = this.getSortedLanguages();
  }

  getActiveLanguages(): abp.localization.ILanguageInfo[] {
    return _.filter(abp.localization.languages, (x) => !x.isDisabled);
  }

  getDefaultLanguage(
    activeLanguages: abp.localization.ILanguageInfo[] = null
  ): abp.localization.ILanguageInfo {
    if (!activeLanguages) {
      activeLanguages = this.getActiveLanguages();
    }

    return _.find(activeLanguages, (x) => x.isDefault);
  }

  getSortedLanguages(): abp.localization.ILanguageInfo[] {
    const currentLanguage = abp.localization.currentLanguage;
    let languages = [currentLanguage];

    const activeLanguages = this.getActiveLanguages();
    const defaultLanguage = this.getDefaultLanguage(activeLanguages);

    if (currentLanguage.name != defaultLanguage.name) {
      languages.push(defaultLanguage);
    }

    const restOfActiveLanguages = _.filter(
      activeLanguages,
      (x) => x.name != currentLanguage.name && x.name != defaultLanguage.name
    );

    if (restOfActiveLanguages) {
      languages.push(...restOfActiveLanguages);
    }

    return languages;
  }

  prepareTranslationModels<TTranslation extends IModelTranslation>(
    translationType: { new (...args: any[]): TTranslation },
    configurer: (translation: TTranslation) => TTranslation = null
  ): TTranslation[] {
    debugger;
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

import { Injectable } from '@angular/core';
import { IModelTranslation } from './multi-lingual.models';
import * as _ from 'lodash';

@Injectable()
export class MultiLingualModelService {
  languages: abp.localization.ILanguageInfo[] = [];

  constructor() {
    this.bindLanguages();
  }

  private bindLanguages(): void {
    const currentLanguage = abp.localization.currentLanguage;
    this.languages = [currentLanguage];

    _.forEach(abp.localization.languages, (language) => {
      if (!language.isDisabled && language.name !== currentLanguage.name) {
        this.languages.push(language);
      }
    });
  }

  prepareTranslationModels<TTranslation extends IModelTranslation>(
    configurer: (
      translation: TTranslation,
      language: string
    ) => TTranslation = null
  ): TTranslation[] {
    const models: TTranslation[] = [];

    // always null for default tranlation
    const defaultModel = { language: null } as TTranslation;
    if (configurer) configurer.call(this, defaultModel, defaultModel.language);
    models.push(defaultModel);

    _.forEach(this.languages, (language) => {
      const model = { language: language.name } as TTranslation;
      if (configurer) configurer.call(this, model, model.language);
      models.push(model);
    });

    return models;
  }
}

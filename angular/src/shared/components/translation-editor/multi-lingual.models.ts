export interface ITranslationModel {
  language: string;
}
export interface IMultiLingualModel<TTranslation extends ITranslationModel> {
  translations: TTranslation[];
}

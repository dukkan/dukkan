export interface IModelTranslation {
  language: string;
}
export interface IMultiLingualModel<TTranslation extends IModelTranslation> {
  translations: TTranslation[];
}

import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ApiResponseModel } from '../../../api/api';
import { bingImagesActions, BingImagesActions } from '../actions/bingImages';

export const isLoadingCollectImages = createReducer<boolean, BingImagesActions>(
    false,
)
    .handleAction(
        [bingImagesActions.collectImages.request],
        (state, action) => true,
    )
    .handleAction(
        [
            bingImagesActions.collectImages.success,
            bingImagesActions.collectImages.failure,
        ],
        (state, action) => false,
    );

export const collectResult = createReducer<
    ApiResponseModel | null,
    BingImagesActions
>(null)
    .handleAction(
        [bingImagesActions.collectImages.success],
        (state, action) => action.payload,
    )
    .handleAction(
        [
            bingImagesActions.collectImages.request,
            bingImagesActions.collectImages.failure,
            bingImagesActions.resetCollectImagesError,
        ],
        (_, __) => null,
    );

export const collectImagesError = createReducer<
    ApiResponseModel | null,
    BingImagesActions
>(null)
    .handleAction(
        [
            bingImagesActions.collectImages.request,
            bingImagesActions.collectImages.success,

            bingImagesActions.resetCollectImagesError,
        ],
        (state, action) => null,
    )
    .handleAction(
        [bingImagesActions.collectImages.failure],
        (state, action) => action.payload,
    );

const bingImagesState = combineReducers({
    isLoadingCollectImages,
    collectResult,
    collectImagesError,
});

export default bingImagesState;
export type BingImagesState = ReturnType<typeof bingImagesState>;

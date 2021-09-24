import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ObjectApiResponseModel, ImageItemModel } from '../../../api/api';
import { imagesActions, ImagesActions } from '../actions/images';

export const isLoadingImages = createReducer<boolean, ImagesActions>(false)
    .handleAction([imagesActions.loadImages.request], (state, action) => true)
    .handleAction(
        [imagesActions.loadImages.success, imagesActions.loadImages.failure],
        (state, action) => false,
    );

export const loadImagesError = createReducer<
    ObjectApiResponseModel | null,
    ImagesActions
>(null)
    .handleAction(
        [
            imagesActions.loadImages.request,
            imagesActions.loadImages.success,

            imagesActions.resetLoadImagesError,
        ],
        (state, action) => null,
    )
    .handleAction(
        [imagesActions.loadImages.failure],
        (state, action) => action.payload,
    );

export const images = createReducer<
    ImageItemModel[] | undefined,
    ImagesActions
>([]).handleAction([imagesActions.loadImages.success], (state, action) => {
    if (action.payload.data?.currentPage === 1) {
        return action.payload.data.items ?? [];
    } else {
        return [...(state ?? []).concat(...(action.payload.data?.items ?? []))];
    }
});

export const hasMoreImages = createReducer<boolean, ImagesActions>(true)
    .handleAction([imagesActions.loadImages.success], (state, action) => {
        const itemsCount = (action.payload.data?.items ?? []).length;
        const limit = action.payload.data?.limit ?? 0;
        return limit === itemsCount;
    })
    .handleAction(
        [imagesActions.loadImages.request, imagesActions.loadImages.failure],
        (state, action) => true,
    );

export const fullSizeImage = createReducer<
    ImageItemModel | null,
    ImagesActions
>(null)
    .handleAction(
        [imagesActions.showFullSizeImage],
        (state, action) => action.payload,
    )
    .handleAction([imagesActions.hideFullSizeImage], (state, action) => null);

const imagesState = combineReducers({
    isLoadingImages,
    images,
    hasMoreImages,
    fullSizeImage,
});

export default imagesState;
export type ImagesState = ReturnType<typeof imagesState>;

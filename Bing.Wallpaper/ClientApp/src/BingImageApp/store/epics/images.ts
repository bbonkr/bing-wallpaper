import { combineEpics, Epic } from 'redux-observable';
import { isActionOf } from 'typesafe-actions';
import { from, of } from 'rxjs';
import { filter, map, switchMap, catchError } from 'rxjs/operators';
import imagesActions, { ImagesActionTypes } from '../actions/images';
import { RootState } from '../reducers';
import { ApiResponseModel } from '../../models';
import { Services } from '../../services';

export const loadImagesEpic: Epic<
    ImagesActionTypes,
    ImagesActionTypes,
    RootState,
    Services
> = (action$, state$, api) =>
    action$.pipe(
        filter(isActionOf(imagesActions.loadImages.request)),
        switchMap((action) =>
            from(api.images.getImages(action.payload)).pipe(
                map(imagesActions.loadImages.success),
                catchError((error: ApiResponseModel) =>
                    of(imagesActions.loadImages.failure(error)),
                ),
            ),
        ),
    );

const imagesEpic = combineEpics(loadImagesEpic);

export default imagesEpic;
export type ImagesEpic = ReturnType<typeof imagesEpic>;

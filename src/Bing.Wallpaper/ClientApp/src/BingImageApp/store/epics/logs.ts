import { combineEpics, Epic } from 'redux-observable';
import { isActionOf } from 'typesafe-actions';
import { from, of } from 'rxjs';
import { filter, map, switchMap, catchError } from 'rxjs/operators';
import { logsActions, LogsActions } from '../actions/logs';
import { RootState } from '../reducers';
import { ApiClient, ErrorResponse } from '../../services';

export const loadLogsEpic: Epic<
    LogsActions,
    LogsActions,
    RootState,
    ApiClient
> = (action$, state$, api) =>
    action$.pipe(
        filter(isActionOf([logsActions.loadLogs.request])),
        switchMap((action) => {
            const { page, take, level, keyword } = action.payload;
            return from(
                api.logs.apiv10LogsGetAll(
                    page,
                    take,
                    level,
                    keyword,
                    undefined,
                ),
            ).pipe(
                map((response) => logsActions.loadLogs.success(response.data)),
                catchError((errorResponse: ErrorResponse) =>
                    of(logsActions.loadLogs.failure(errorResponse.data)),
                ),
            );
        }),
    );

const imagesEpic = combineEpics(loadLogsEpic);

export default imagesEpic;
export type ImagesEpic = ReturnType<typeof imagesEpic>;

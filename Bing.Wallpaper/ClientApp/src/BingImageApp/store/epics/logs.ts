import { combineEpics, Epic } from 'redux-observable';
import { isActionOf } from 'typesafe-actions';
import { from, of } from 'rxjs';
import { filter, map, switchMap, catchError } from 'rxjs/operators';
import { logsActions, LogsActions } from '../actions/logs';
import { RootState } from '../reducers';
import { ApiResponseModel } from '../../models';
import { Services } from '../../services';

export const loadLogsEpic: Epic<LogsActions, LogsActions, RootState, Services> =
    (action$, state$, api) =>
        action$.pipe(
            filter(isActionOf([logsActions.loadLogs.request])),
            switchMap((action) =>
                from(api.logs.getLogs(action.payload)).pipe(
                    map(logsActions.loadLogs.success),
                    catchError((error: ApiResponseModel) =>
                        of(logsActions.loadLogs.failure(error)),
                    ),
                ),
            ),
        );

export const appendLogsEpic: Epic<
    LogsActions,
    LogsActions,
    RootState,
    Services
> = (action$, state$, api) =>
    action$.pipe(
        filter(isActionOf([logsActions.appendLogs.request])),
        switchMap((action) =>
            from(api.logs.getLogs(action.payload)).pipe(
                map(logsActions.appendLogs.success),
                catchError((error: ApiResponseModel) =>
                    of(logsActions.appendLogs.failure(error)),
                ),
            ),
        ),
    );

const imagesEpic = combineEpics(loadLogsEpic, appendLogsEpic);

export default imagesEpic;
export type ImagesEpic = ReturnType<typeof imagesEpic>;

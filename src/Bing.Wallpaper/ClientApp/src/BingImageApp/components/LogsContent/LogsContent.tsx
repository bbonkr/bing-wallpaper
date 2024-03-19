import React, { useEffect, useState } from 'react';
import dayjs from 'dayjs';
import { Content, Section } from '../Layouts';
import { LogFilter, FormState } from './LogFilter';
import useSWRInfinite from 'swr/infinite';
import { ApiClient } from '../../services';

import './style.css';

export const LogsContent = () => {
    const COLUMNS_COUNT = 3;
    const take = 10;
    const [formState, setFormState] = useState<FormState>();
    const [hasMoreLogs, setHasMoreLogs] = useState(true);

    const { data, error, isValidating, size, setSize } = useSWRInfinite(
        (index: number) => {
            const page = index + 1;

            return [
                `/api/logs?page=${page}&level=${
                    formState?.values.level ?? ''
                }&keyword=${formState?.values.keyword ?? ''}`,
                page,
                formState?.values.level,
                formState?.values.keyword,
            ];
        },
        (
            _: any,
            page: number,
            level: string | undefined,
            keyword: string | undefined,
        ) => {
            return new ApiClient().logs
                .apiv10LogsGetAll({ page, take, level, keyword })
                .then((res) => res.data.data);
        },
    );

    const handleClickLoadMore = () => {
        if (hasMoreLogs) {
            setSize((prevSize) => prevSize + 1);
        }
    };

    const handleSubmit = (formState: FormState) => {
        setFormState((_) => ({
            ...formState,
        }));

        setSize((_) => 1);
    };

    useEffect(() => {
        if (data) {
            const latestSet = data.find(
                (_, index, arr) => index === arr.length - 1,
            );
            setHasMoreLogs((prevState) => {
                let endOfList = false;

                if (latestSet) {
                    endOfList = latestSet.currentPage === latestSet.totalPages;
                }
                const hasMore = !endOfList;
                if (prevState !== hasMore) {
                    return hasMore;
                }
                return prevState;
            });
        }
    }, [data]);

    useEffect(() => {
        if (error) {
            console.error(
                '[ERROR] Error occurred when data has been fetching.',
                error,
            );
        }
    }, [error]);

    return (
        <Content classNames={[]}>
            <Section
                title="Logs"
                useHero
                heroSize="is-small"
                heroColor="is-info"
            />
            <Section>
                <LogFilter onSubmit={handleSubmit} isLoading={isValidating} />
            </Section>
            <Section classNames={[]}>
                <div className="table-container">
                    <table className="table is-hoverable log-table">
                        <thead>
                            <tr className="">
                                <th scope="col" className="">
                                    @Logged
                                </th>
                                <th scope="col" className="">
                                    Level
                                </th>
                                <th scope="col">Message</th>
                            </tr>
                        </thead>
                        <tbody>
                            {data && data.length > 0 ? (
                                data.map((responseData) =>
                                    responseData?.items?.map((log) => (
                                        <tr key={log.id}>
                                            <td>
                                                {dayjs(log.timeStamp).format(
                                                    'YYYY-MM-DD HH:mm:ss',
                                                )}
                                            </td>
                                            <td>{log.level}</td>
                                            <td className="content-wrap">
                                                {log.message}
                                            </td>
                                        </tr>
                                    )),
                                )
                            ) : (
                                <tr>
                                    <td
                                        className="has-text-centered"
                                        colSpan={COLUMNS_COUNT}
                                    >
                                        Did not find anything. Please try
                                        another keyword.
                                    </td>
                                </tr>
                            )}
                        </tbody>
                        {hasMoreLogs && (
                            <tfoot>
                                <tr>
                                    <td
                                        colSpan={COLUMNS_COUNT}
                                        className="pt-6 pb-6"
                                    ></td>
                                </tr>
                            </tfoot>
                        )}
                    </table>
                </div>

                <div>
                    <button
                        className={`button is-fullwidth ${
                            isValidating ? 'is-loading' : ''
                        }`}
                        onClick={handleClickLoadMore}
                        disabled={isValidating || !hasMoreLogs}
                        title="Load more logs"
                    >
                        <span>
                            {hasMoreLogs
                                ? 'Load more'
                                : 'End of the image list'}
                        </span>
                    </button>
                </div>
            </Section>
        </Content>
    );
};

export default LogsContent;

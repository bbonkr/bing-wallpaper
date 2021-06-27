import React, { useEffect, useState } from 'react';
import { Content, Section } from '../Layouts';
import { useLogsApi } from '../../hooks/useLogsApi';
import { LogFilter, FormState } from './LogFilter';
import './style.css';

export const LogsContent = () => {
    const COLUMNS_COUNT = 3;
    const [page, setPage] = useState(1);
    const take = 10;
    const [formState, setFormState] = useState<FormState>();

    const { logs, isLoadingLogs, hasMoreLogs, loadLogsRequest } = useLogsApi();

    const handleClickLoadMore = () => {
        if (hasMoreLogs) {
            loadLogsRequest({
                page: page + 1,
                take: take,
                keyword: formState?.values.keyword ?? '',
                level: formState?.values.level ?? '',
            });
            setPage((state) => state + 1);
        }
    };

    const handleSubmit = (formState: FormState) => {
        const { keyword, level } = formState.values;
        loadLogsRequest({
            page: 1,
            take: take,
            keyword: keyword,
            level: level,
        });

        setPage((_) => 1);
        setFormState((prevState) => ({
            ...formState,
        }));
    };

    useEffect(() => {
        if ((logs ?? []).length === 0) {
            loadLogsRequest({ page: page, take: take, keyword: '', level: '' });
            setPage((_) => 1);
        }
    }, []);

    return (
        <Content classNames={[]}>
            <Section
                title="Logs"
                useHero
                heroSize="is-small"
                heroColor="is-info"
            />
            <Section>
                <LogFilter onSubmit={handleSubmit} isLoading={isLoadingLogs} />
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
                            {logs && logs.length > 0 ? (
                                logs.map((log) => (
                                    <tr key={log.id}>
                                        <td>{log.logged}</td>
                                        <td>{log.level}</td>
                                        <td>{log.message}</td>
                                    </tr>
                                ))
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
                            isLoadingLogs ? 'is-loading' : ''
                        }`}
                        onClick={handleClickLoadMore}
                        disabled={isLoadingLogs}
                        title="Load more logs"
                    >
                        Load more
                    </button>
                </div>
            </Section>
        </Content>
    );
};

import React, { useEffect, useState } from 'react';
import { ImagesList, ListContainer } from './ImagesList';
import { Content, Section } from '../Layouts';
import { FaSync } from 'react-icons/fa';
import useSWRInfinite from 'swr/infinite';
import { ApiClient } from '../../services';

import Loading from '../Loading';

export const ImagesContent = () => {
    const [hasMoreImages, setHasMoreImages] = useState(true);

    const take = 10;

    const { data, error, isValidating, size, setSize } = useSWRInfinite(
        (index: number) => {
            const page = index + 1;
            return [`/api/images?page=${page}`, page];
        },
        (_: any, page: number) =>
            new ApiClient().images
                .apiv10ImagesGetAll({ page, take })
                .then((res) => res.data),
        {},
    );

    const handleClickLoadMore = () => {
        if (hasMoreImages) {
            setSize((prevSize) => prevSize + 1);
        }
    };

    const handleClickRefresh = () => {
        setSize((_) => 1);
    };

    useEffect(() => {
        if (data) {
            const latestSet = data.find(
                (_, index, arr) => index === arr.length - 1,
            );

            setHasMoreImages((prevState) => {
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
        if (error)
            console.error(
                `[ERROR] Error occurred when data has been fetching.`,
                error,
            );
    }, [error]);

    useEffect(() => {
        let timeout: number | undefined = undefined;

        const handleScroll = () => {
            if (typeof timeout === 'number') {
                window.clearTimeout(timeout);
            }
            timeout = window.setTimeout(() => {
                const position =
                    (window.innerHeight + window.scrollY) /
                    window.document.body.clientHeight;

                if (position > 0.9 && hasMoreImages) {
                    setSize((prevSize) => prevSize + 1);
                }
            }, 200);
        };

        if (hasMoreImages) {
            window.addEventListener('scroll', handleScroll);
            window.addEventListener('resize', handleScroll);

            handleScroll();
        } else {
            if (typeof timeout === 'number') {
                window.clearTimeout(timeout);
            }
            window.removeEventListener('scroll', handleScroll);
            window.removeEventListener('resize', handleScroll);
        }

        return () => {
            if (typeof timeout === 'number') {
                window.clearInterval(timeout);
            }
            window.removeEventListener('scroll', handleScroll);
            window.removeEventListener('resize', handleScroll);
        };
    }, [hasMoreImages]);

    return (
        <Content classNames={[]}>
            <Section
                title={
                    <React.Fragment>
                        Bing Today Images{' '}
                        <button
                            className={`button ${
                                isValidating ? 'is-loading' : ''
                            }`}
                            disabled={isValidating}
                            onClick={handleClickRefresh}
                            title="Reload images"
                        >
                            <FaSync />
                        </button>
                    </React.Fragment>
                }
                useHero
                heroColor="is-primary"
                heroSize="is-small"
            />

            <Section classNames={[]}>
                <ListContainer>
                    {data ? (
                        data.map((images, index) => {
                            if (images && images.items) {
                                return (
                                    <ImagesList
                                        key={index}
                                        images={images.items}
                                    />
                                );
                            }
                        })
                    ) : (
                        <Loading />
                    )}
                </ListContainer>
                <ListContainer></ListContainer>

                <div className="is-flex is-flex-direction-column is-justify-content-center">
                    <button
                        className={`button ${isValidating ? 'is-loading' : ''}`}
                        disabled={isValidating || !hasMoreImages}
                        onClick={handleClickLoadMore}
                        title="Load more images"
                    >
                        <span>
                            {hasMoreImages
                                ? 'Load more'
                                : 'End of the image list'}
                        </span>
                    </button>
                </div>
            </Section>
        </Content>
    );
};

export default ImagesContent;

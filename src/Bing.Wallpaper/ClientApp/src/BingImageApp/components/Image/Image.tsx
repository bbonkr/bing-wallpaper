import React, { useEffect, useState, useRef } from 'react';

import './style.css';

export interface ImageProps {
    imgProps?: React.ImgHTMLAttributes<HTMLImageElement>;
    imageSrc?: string;
    imageThumbnailSrc?: string;
    isRequestFullScreen?: boolean;
    onClick?: () => void;
    onLoaded?: (el?: HTMLImageElement | null) => void;
    onRequestedFullscreen?: () => void;
}

export const Image = ({
    imageSrc,
    imageThumbnailSrc,
    isRequestFullScreen,
    imgProps,
    onClick,
    onRequestedFullscreen,
    onLoaded,
}: ImageProps) => {
    const [imageLoaded, setImageLoaded] = useState(false);
    const [thumbnailLoaded, setThumbnailLoaded] = useState(false);

    const imageRef = useRef<HTMLImageElement>(null);
    const observerRef = useRef<IntersectionObserver>();

    const handleImageLoaded = (
        event: React.SyntheticEvent<HTMLImageElement, Event>,
    ) => {
        event.persist();
        setThumbnailLoaded((_) => true);
    };

    const handleIntersection = (
        entries: IntersectionObserverEntry[],
        intersectionObserver: IntersectionObserver,
    ) => {
        entries.forEach((entry) => {
            if (entry.isIntersecting && thumbnailLoaded) {
                intersectionObserver.unobserve(entry.target);
                setImageLoaded((_) => true);
                // console.info(
                //     '🔨 Request image file: ',
                //     imageRef?.current?.src ||
                //         imageRef?.current?.srcset ||
                //         'Does not find image file uri',
                // );
            }
        });
    };

    useEffect(() => {
        if (thumbnailLoaded) {
            const imgEl = imageRef.current;

            if (!observerRef.current) {
                observerRef.current = new IntersectionObserver(
                    handleIntersection,
                    {
                        threshold: 0.4,
                    },
                );
            }

            if (imgEl) {
                observerRef.current.observe(imgEl);
            }

            if (onLoaded) {
                onLoaded(imgEl);
            }
        }

        return () => {
            if (observerRef.current) {
                observerRef.current.disconnect();
            }
        };
    }, [thumbnailLoaded]);

    useEffect(() => {
        if (typeof isRequestFullScreen === 'boolean' && isRequestFullScreen) {
            // console.info(
            //     'Image => ',
            //     typeof imageRef.current?.requestFullscreen,
            // );
            if (typeof imageRef.current?.requestFullscreen === 'function') {
                imageRef.current?.requestFullscreen();
            }
            if (onRequestedFullscreen) {
                onRequestedFullscreen();
            }
        }
    }, [isRequestFullScreen]);

    return (
        <img
            {...imgProps}
            ref={imageRef}
            src={imgProps?.src ?? imageLoaded ? imageSrc : imageThumbnailSrc}
            className={`${imgProps?.className ?? ''} ${
                imageSrc && imageThumbnailSrc ? 'lazy-load-image' : ''
            } ${imageLoaded ? 'loaded' : ''} ${onClick ? 'is-clickable' : ''}`}
            onLoad={handleImageLoaded}
            onClick={onClick}
        />
    );
};

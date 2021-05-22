import React, { useRef, useEffect, useState } from 'react';
import { useImagesApi } from '../../hooks/useImagesApi';
import { ImageItemModel } from '../../models';
import './style.css';

interface ImageCardProps {
    image: ImageItemModel;
}

export const ImageCard = ({ image }: ImageCardProps) => {
    const { showFullSizeImage } = useImagesApi();
    const [imageLoaded, setImageLoaded] = useState(false);
    const [thumbnailLoaded, setThumbnailLoaded] = useState(false);

    const imageRef = useRef<HTMLImageElement>(null);
    const observerRef = useRef<IntersectionObserver>();

    const handleClickImage = () => {
        showFullSizeImage(image);
    };

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
                console.info(
                    '🔨 Request image file: ',
                    imageRef?.current?.src ||
                        imageRef?.current?.srcset ||
                        'Does not find image file uri',
                );
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
        }
        return () => {
            if (observerRef.current) {
                observerRef.current.disconnect();
            }
        };
    }, [thumbnailLoaded]);

    return (
        <div
            className="column is-two-thirds-tablet is-half-desktop is-one-third-widescreen is-one-quarter-fullhd clickable "
            onClick={handleClickImage}
        >
            <figure className="is-position-relative">
                <img
                    ref={imageRef}
                    src={
                        imageLoaded
                            ? `/api/v1.0/files/${encodeURIComponent(
                                  `${image.fileName}`,
                              )}`
                            : `/api/v1.0/files/${encodeURIComponent(
                                  `${image.fileName}`,
                              )}?type=thumbnail`
                    }
                    className={imageLoaded ? 'loaded' : ''}
                    title={image.title ?? image.fileName}
                    alt={image.title ?? image.fileName}
                    onLoad={handleImageLoaded}
                />
                <figcaption className="card-title-over-image rounded-bottom">
                    {image.title ?? image.fileName}
                </figcaption>
            </figure>
        </div>
    );
};

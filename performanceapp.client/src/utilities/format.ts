export const formatSEK = (value: number) =>
    new Intl.NumberFormat("sv-SE", {
        style: "currency",
        currency: "SEK",
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    }).format(value);

export const formatInt = (value: number) =>
    new Intl.NumberFormat("sv-SE", {
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    }).format(value);

export const formatPercent = (value: number | null): string => {
    if (value == null) {
        return "";
    }

    return new Intl.NumberFormat("sv-SE", {
        style: "percent",
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
    }).format(value);
};

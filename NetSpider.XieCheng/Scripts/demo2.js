function o(t, e) {
    var r = (65535 & t) + (65535 & e);
    return (t >> 16) + (e >> 16) + (r >> 16) << 16 | 65535 & r
}

function i(t, e, r, n, i, s) {
    return o((u = o(o(e, t), o(n, s))) << (c = i) | u >>> 32 - c, r);
    var u, c
}

function s(t, e, r, n, o, s, u) {
    return i(e & r | ~e & n, t, e, o, s, u)
}

function u(t, e, r, n, o, s, u) {
    return i(e & n | r & ~n, t, e, o, s, u)
}

function c(t, e, r, n, o, s, u) {
    return i(e ^ r ^ n, t, e, o, s, u)
}

function a(t, e, r, n, o, s, u) {
    return i(r ^ (e | ~n), t, e, o, s, u)
}

function f(t, e) {
    var r, n, i, f, l;
    t[e >> 5] |= 128 << e % 32,
        t[14 + (e + 64 >>> 9 << 4)] = e;
    var h = 1732584193,
        p = -271733879,
        d = -1732584194,
        v = 271733878;
    for (r = 0; r < t.length; r += 16)
        h = s(n = h, i = p, f = d, l = v, t[r], 7, -680876936),
        v = s(v, h, p, d, t[r + 1], 12, -389564586),
        d = s(d, v, h, p, t[r + 2], 17, 606105819),
        p = s(p, d, v, h, t[r + 3], 22, -1044525330),
        h = s(h, p, d, v, t[r + 4], 7, -176418897),
        v = s(v, h, p, d, t[r + 5], 12, 1200080426),
        d = s(d, v, h, p, t[r + 6], 17, -1473231341),
        p = s(p, d, v, h, t[r + 7], 22, -45705983),
        h = s(h, p, d, v, t[r + 8], 7, 1770035416),
        v = s(v, h, p, d, t[r + 9], 12, -1958414417),
        d = s(d, v, h, p, t[r + 10], 17, -42063),
        p = s(p, d, v, h, t[r + 11], 22, -1990404162),
        h = s(h, p, d, v, t[r + 12], 7, 1804603682),
        v = s(v, h, p, d, t[r + 13], 12, -40341101),
        d = s(d, v, h, p, t[r + 14], 17, -1502002290),
        h = u(h, p = s(p, d, v, h, t[r + 15], 22, 1236535329), d, v, t[r + 1], 5, -165796510),
        v = u(v, h, p, d, t[r + 6], 9, -1069501632),
        d = u(d, v, h, p, t[r + 11], 14, 643717713),
        p = u(p, d, v, h, t[r], 20, -373897302),
        h = u(h, p, d, v, t[r + 5], 5, -701558691),
        v = u(v, h, p, d, t[r + 10], 9, 38016083),
        d = u(d, v, h, p, t[r + 15], 14, -660478335),
        p = u(p, d, v, h, t[r + 4], 20, -405537848),
        h = u(h, p, d, v, t[r + 9], 5, 568446438),
        v = u(v, h, p, d, t[r + 14], 9, -1019803690),
        d = u(d, v, h, p, t[r + 3], 14, -187363961),
        p = u(p, d, v, h, t[r + 8], 20, 1163531501),
        h = u(h, p, d, v, t[r + 13], 5, -1444681467),
        v = u(v, h, p, d, t[r + 2], 9, -51403784),
        d = u(d, v, h, p, t[r + 7], 14, 1735328473),
        h = c(h, p = u(p, d, v, h, t[r + 12], 20, -1926607734), d, v, t[r + 5], 4, -378558),
        v = c(v, h, p, d, t[r + 8], 11, -2022574463),
        d = c(d, v, h, p, t[r + 11], 16, 1839030562),
        p = c(p, d, v, h, t[r + 14], 23, -35309556),
        h = c(h, p, d, v, t[r + 1], 4, -1530992060),
        v = c(v, h, p, d, t[r + 4], 11, 1272893353),
        d = c(d, v, h, p, t[r + 7], 16, -155497632),
        p = c(p, d, v, h, t[r + 10], 23, -1094730640),
        h = c(h, p, d, v, t[r + 13], 4, 681279174),
        v = c(v, h, p, d, t[r], 11, -358537222),
        d = c(d, v, h, p, t[r + 3], 16, -722521979),
        p = c(p, d, v, h, t[r + 6], 23, 76029189),
        h = c(h, p, d, v, t[r + 9], 4, -640364487),
        v = c(v, h, p, d, t[r + 12], 11, -421815835),
        d = c(d, v, h, p, t[r + 15], 16, 530742520),
        h = a(h, p = c(p, d, v, h, t[r + 2], 23, -995338651), d, v, t[r], 6, -198630844),
        v = a(v, h, p, d, t[r + 7], 10, 1126891415),
        d = a(d, v, h, p, t[r + 14], 15, -1416354905),
        p = a(p, d, v, h, t[r + 5], 21, -57434055),
        h = a(h, p, d, v, t[r + 12], 6, 1700485571),
        v = a(v, h, p, d, t[r + 3], 10, -1894986606),
        d = a(d, v, h, p, t[r + 10], 15, -1051523),
        p = a(p, d, v, h, t[r + 1], 21, -2054922799),
        h = a(h, p, d, v, t[r + 8], 6, 1873313359),
        v = a(v, h, p, d, t[r + 15], 10, -30611744),
        d = a(d, v, h, p, t[r + 6], 15, -1560198380),
        p = a(p, d, v, h, t[r + 13], 21, 1309151649),
        h = a(h, p, d, v, t[r + 4], 6, -145523070),
        v = a(v, h, p, d, t[r + 11], 10, -1120210379),
        d = a(d, v, h, p, t[r + 2], 15, 718787259),
        p = a(p, d, v, h, t[r + 9], 21, -343485551),
        h = o(h, n),
        p = o(p, i),
        d = o(d, f),
        v = o(v, l);
    return [h, p, d, v]
}

function l(t) {
    var e, r = "",
        n = 32 * t.length;
    for (e = 0; e < n; e += 8)
        r += String.fromCharCode(t[e >> 5] >>> e % 32 & 255);
    return r
}

function h(t) {
    var e, r = [];
    for (r[(t.length >> 2) - 1] = void 0, e = 0; e < r.length; e += 1)
        r[e] = 0;
    var n = 8 * t.length;
    for (e = 0; e < n; e += 8)
        r[e >> 5] |= (255 & t.charCodeAt(e / 8)) << e % 32;
    return r
}

function p(t) {
    var e, r, n = "0123456789abcdef",
        o = "";
    for (r = 0; r < t.length; r += 1)
        e = t.charCodeAt(r),
        o += n.charAt(e >>> 4 & 15) + n.charAt(15 & e);
    return o
}

function d(t) {
    var a1 = encodeURIComponent(t);
    var a2 = unescape(a1);
    return a2;
}

function v(t) {
    return l(f(h(e = d(t)), 8 * e.length));
    var e
}

function y(t, e) {
    return function (t, e) {
        var r, n, o = h(t),
            i = [],
            s = [];
        for (i[15] = s[15] = void 0,
            16 < o.length && (o = f(o, 8 * t.length)),
            r = 0; r < 16; r += 1)
            i[r] = 909522486 ^ o[r],
            s[r] = 1549556828 ^ o[r];
        return n = f(i.concat(h(e)), 512 + 8 * e.length),
            l(f(s.concat(n), 640))
    }(d(t), d(e))
}

function m(t, e, r) {
    return e ? r ? y(e, t) : p(y(e, t)) : r ? v(t) : p(v(t))
}


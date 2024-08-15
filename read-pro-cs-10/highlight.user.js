// ==UserScript==
// @name        Helpers for https://learning.oreilly.com
// @namespace   Violentmonkey Scripts
// @match       https://learning.oreilly.com/library/view/pro-c-10/9781484278697/html/*
// @grant       none
// @version     1.0
// @author      letientai299
// ==/UserScript==

function loadHighlightJS() {
  const prefix = "https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0";
  const css = document.createElement("link");
  css.rel = "stylesheet";
  // https://github.com/highlightjs/highlight.js/tree/main/src/styles
  css.href = `${prefix}/styles/atom-one-dark.min.css`;
  document.head.appendChild(css);

  const hljs = document.createElement("script");
  hljs.src = `${prefix}/highlight.min.js`;
  document.head.appendChild(hljs);
}

function makePreCode(txt) {
  const code = document.createElement("code");
  txt = txt.replace(/[^\S\n]{2}/g, "  ");
  code.textContent = `\r${txt}`;

  const pre = document.createElement("pre");
  pre.appendChild(code);
  return pre;
}

loadHighlightJS();
tryHighlight();

function tryHighlight() {
  let cnt = 0;
  const timer = setInterval(loop, 100);
  function loop() {
    cnt++;
    // stop after 100 attempts, which is 10s
    if (highlighted() || cnt > 100) {
      clearInterval(timer);
    }
  }
}

function highlighted() {
  const codes = [...document.getElementsByClassName("ProgramCode")];
  console.log("found ", codes.length);
  if (codes.length == 0) {
    return false;
  }

  codes.forEach((div) => {
    const pre = makePreCode(div.innerText);
    div.parentNode.replaceChild(pre, div);
    hljs.highlightElement(pre);
  });
  return true;
}

// also try to highlight code on href change
// https://stackoverflow.com/a/46428962/3869533
const observeUrlChange = (action) => () => {
  let oldHref = document.location.href;
  const body = document.querySelector("body");
  const observer = new MutationObserver(() => {
    if (oldHref !== document.location.href) {
      oldHref = document.location.href;
      action();
    }
  });
  observer.observe(body, { childList: true, subtree: true });
};

window.onload = observeUrlChange(tryHighlight);

[merge]
	tool = kdiff3
[mergetool "kdiff3"]
	path = C:/Program Files/KDiff3/kdiff3.exe
[diff]
	guitool = kdiff3
	algorithm = patience
[difftool "kdiff3"]
	path = C:/Program Files/KDiff3/kdiff3.exe
	cmd = \"C:/Program Files/KDiff3/kdiff3.exe\" \"$LOCAL\" \"$REMOTE\"
[core]
	editor = \"C:/Program Files (x86)/GitExtensions/GitExtensions.exe\" fileeditor
	autocrlf = true
[user]
	name = your.name
	email = your.email
[mergetool]
	keepBackup = false
[alias]
	xxx = "!f() { find . -name .git -type d -execdir git clean -xdf \\; ; }; f"
	amend = commit -a --amend
	co = checkout
	cob = checkout -b
	s = status -sb
	l = log --graph --pretty=format:\"%C(yellow)%h%C(cyan)%d%Creset %s %C(white)- %an, %ar%Creset\"
	ls = log --pretty=format:\"%C(yellow)%h%C(cyan)%d%C(green) %s %C(white)- %an, %ar%Creset\" --decorate --numstat
	d = diff --color-words
	wip = !git add . && git commit -m \"WIP\"
	cm = "!git add . && git commit -m "
	undo = reset HEAD~1 --mixed
	save = !git add -A && git commit -m 'SAVEPOINT'
	wipe = !git add -A && git commit -qm \"UNDO SAVEPOINT\" && git reset HEAD~1 --hard
	bclean = "!f() { git branch --merged ${1-master} | grep -v ${1-master}$ | xargs -r git branch -d; }; f"
	delete-merged-branches = "!f() { git checkout --quiet master && git branch --merged | grep --invert-match '\\*' | xargs -n 1 git branch --delete; git checkout --quiet @{-1}; }; f"
	bdone = "!f() { git checkout ${1-master} && git up && git bclean ${1-master}; }; f"
	la = !git config -l | grep alias | cut -c 7-
	b = !git for-each-ref --sort=\"-authordate\" --format=\"%(authordate)%09%(objectname:short)%09%(refname)\" refs/heads | sed -e \"s-refs/heads/--\"
	ready = rebase -i master
	ready-root = rebase -i --root
	up = !git pull --rebase --prune $@ && git submodule update --init --recursive
	bsync = !git pull --rebase=preserve --prune origin master && git submodule update --init --recursive
	upstream = "!f() { git remote add upstream https://github.com/$1.git; }; f"
	syncu = "!f() { git checkout ${1-master} && git fetch upstream && git rebase upstream/master; }; f"
	type = cat-file -t
	dump = cat-file -p
	pv = !git push --tag && git push
	pushit = "!f() { git rev-parse --abbrev-ref HEAD ; }; git push --set-upstream origin `f`"
	r = remote -v
	fixup = !sh -c 'git commit --fixup=$1' -
	squash = !sh -c 'git commit --squash=$1' -
	pr = "!f() { git fetch -fu ${2:-origin} refs/pull/$1/head:pr-$1 && git checkout pr-$1; }; f"
	pr-clean = "!git for-each-ref refs/heads/pr-* --format=\"%(refname)\" | while read ref ; do branch=${ref#refs/heads/} ; git branch -D $branch ; done"
[filter "lfs"]
	smudge = git-lfs smudge -- %f
	process = git-lfs filter-process
	required = true
	clean = git-lfs clean -- %f
[pull]
	rebase = false
[fetch]
	prune = false
[rebase]
	autoStash = false

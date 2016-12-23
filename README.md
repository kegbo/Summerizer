# Summerizer

An automatic text summarization tool based on the number of occurrence of words
method

 Count the occurrence of different words in the document, copying the words into a list
which is ordered by frequency with the most common word at the top of the list.
 Copy each sentence in the document into a second list.
 Remove (filter) those words from the word frequency list which are very common and
of little use in classifying the document. These are called ‘stop words’ and include
words such as such as the, is, at, which, and on. There is no definitive listing of what
defines a stop word as it could be document specific, however the file ‘stopwords.txt’
is provided containing a listing of generic common stop words (taken from: 
http://www.lextek.com/manuals/onix/stopwords1.html ). Its use is optional, and you
may edit it or use an alternative list of stop words or not use a stop word list at all.
Longer and shorter alternative lists are given at: http://www.ranks.nl/stopwords.
 Repeat the following until SF is exceeded>
{
 For each sentence, count the number of the words that matches the top word
(most frequent) in the filtered word list.
 Find the sentence that has the highest number of occurrences of the most
frequent word.
 If the length of words in the summary text added to the current selected
sentence word length exceeds the summary word length limit the sentence is
ignored.
 Else add the sentence to the summary text. Remove the word from the top of
the frequency word list and the sentence from the listing of word sentences.
}
 Output summary text to file and display appropriate statistics, including actual SF.

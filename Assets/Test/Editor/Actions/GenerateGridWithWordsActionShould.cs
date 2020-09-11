﻿using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GenerateGridWithWordsActionShould
    {
        private GenerateGridWithWordsAction action;

        private AddWordsService addWordsService;
        private FillGridService fillGridService;

        private Words wordsRepository;
        private SomeRandomQueuedPositionGenerator ramdomPositionGenerator;
        private IShuffleWordsService shuffleWordsService;

        [SetUp]
        public void Setup()
        {
            fillGridService = new FillGridService();

            wordsRepository = new InMemoryWordsRepository();
            ramdomPositionGenerator = new SomeRandomQueuedPositionGenerator();
            shuffleWordsService = new SomeShuffleWordsService();
            addWordsService = new AddWordsService(wordsRepository, ramdomPositionGenerator, shuffleWordsService);

            action = new GenerateGridWithWordsAction(addWordsService, fillGridService);
        }

        [Test]
        public void GenerateGridWithWordsActionShouldSimplePasses()
        {
            // Given
            wordsRepository.Add(new Word("Uno"));
            wordsRepository.Add(new Word("Dos"));
            wordsRepository.Add(new Word("Tres"));
            wordsRepository.Add(new Word("Cuatro"));
            wordsRepository.Add(new Word("Cinco"));
            ramdomPositionGenerator.SetMaxPosition(new Position(10, 10));
            ramdomPositionGenerator.SetReturnPosition(new Position(0, 0));
            ramdomPositionGenerator.SetReturnPosition(new Position(1, 1));
            ramdomPositionGenerator.SetReturnPosition(new Position(2, 2));
            ramdomPositionGenerator.SetReturnPosition(new Position(3, 3));
            ramdomPositionGenerator.SetReturnPosition(new Position(4, 4));

            // When
            var result = action.Execute(9, 9, 5);

            // Then
            PrintGrid.Print(result);
            Assert.IsTrue(result.GetLeterInPosition(0, 0) == 'U');
            Assert.IsTrue(result.GetLeterInPosition(1, 0) == 'n');
            Assert.IsTrue(result.GetLeterInPosition(2, 0) == 'o');

            Assert.IsTrue(result.GetLeterInPosition(1, 1) == 'D');
            Assert.IsTrue(result.GetLeterInPosition(2, 1) == 'o');
            Assert.IsTrue(result.GetLeterInPosition(3, 1) == 's');

            Assert.IsTrue(result.GetLeterInPosition(2, 2) == 'T');
            Assert.IsTrue(result.GetLeterInPosition(3, 2) == 'r');
            Assert.IsTrue(result.GetLeterInPosition(4, 2) == 'e');
            Assert.IsTrue(result.GetLeterInPosition(5, 2) == 's');

            Assert.IsTrue(result.GetLeterInPosition(3, 3) == 'C');
            Assert.IsTrue(result.GetLeterInPosition(4, 3) == 'u');
            Assert.IsTrue(result.GetLeterInPosition(5, 3) == 'a');
            Assert.IsTrue(result.GetLeterInPosition(6, 3) == 't');
            Assert.IsTrue(result.GetLeterInPosition(7, 3) == 'r');
            Assert.IsTrue(result.GetLeterInPosition(8, 3) == 'o');

            Assert.IsTrue(result.GetLeterInPosition(4, 4) == 'C');
            Assert.IsTrue(result.GetLeterInPosition(5, 4) == 'i');
            Assert.IsTrue(result.GetLeterInPosition(6, 4) == 'n');
            Assert.IsTrue(result.GetLeterInPosition(7, 4) == 'c');
            Assert.IsTrue(result.GetLeterInPosition(8, 4) == 'o');

            Assert.IsTrue(CheckEmptySpaces.Check(result));

            Assert.IsTrue(ramdomPositionGenerator.Count == 0);
        }
    }
}
